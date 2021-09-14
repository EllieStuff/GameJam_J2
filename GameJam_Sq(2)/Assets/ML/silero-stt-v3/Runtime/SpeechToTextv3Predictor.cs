/* 
*   Speech to Text v3
*   Copyright (c) 2021 Yusuf Olokoba.
*/

namespace NatSuite.ML.Audio {

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Internal;
    using Types;

    /// <summary>
    /// Silero Speech-to-Text v3 predictor.
    /// This predictor predicts text from a linear PCM waveform.
    /// </summary>
    public sealed class SpeechToTextv3Predictor : IMLPredictor<string> {

        #region --Client API--
        /// <summary>
        /// Minimum sample buffer size.
        /// </summary>
        public const int MinBufferSize = 12_800;

        /// <summary>
        /// Create a speech-to-text predictor.
        /// </summary>
        /// <param name="model">Speech-to-tect ML model.</param>
        /// <param name="labels">Speech tokens.</param>
        public SpeechToTextv3Predictor (MLModel model, string[] labels) {
            this.model = model;
            this.labels = labels;
            this.blankIdx = Array.IndexOf(labels, "_");
            this.spaceIdx = Array.IndexOf(labels, " ");
            this.twoIdx = Array.IndexOf(labels, "2");
        }

        /// <summary>
        /// Convert speech to text.
        /// </summary>
        /// <param name="inputs">Input audio feature.</param>
        /// <returns>Detected text.</returns>
        public unsafe string Predict (params MLFeature[] inputs) {
            // Check
            if (inputs.Length != 1)
                throw new ArgumentException(@"Speech-to-text predictor expects a single feature", nameof(inputs));
            // Check type
            var input = inputs[0];
            if (!(input.type is MLArrayType type))
                throw new ArgumentException(@"Speech-to-text predictor expects an an array feature", nameof(inputs));
            // Check LPCM
            if (type.dims != 2 || type.dataType != typeof(float)) // 1xF, F=frames
                throw new ArgumentException(@"Speech-to-text predictor expects floating-point linear PCM audio data", nameof(inputs));
            // Check size
            if (type.shape[1] < MinBufferSize)
                throw new ArgumentException($"Speech-to-text predictor requires buffer with size {MinBufferSize} or more", nameof(inputs));
            // Predict
            var inputType = model.inputs[0];
            var inputFeature = (input as IMLFeature).Create(inputType);
            var outputFeature = model.Predict(inputFeature)[0];
            inputFeature.ReleaseFeature();
            // Marshal
            var logitShape = outputFeature.FeatureShape();
            var (tokenCount, logitCount) = (logitShape[1], logitShape[2]);
            var logits = (float*)outputFeature.FeatureData();
            var tokens = new List<string>();
            for (var i = 0; i < tokenCount; ++i) {
                var tokenLogits = &logits[i * logitCount];
                var tokenIdx = Enumerable.Range(0, logitCount).Aggregate((p, q) => tokenLogits[p] > tokenLogits[q] ? p : q);
                if (tokenIdx == twoIdx) {
                    if (tokens.Count == 0)
                        tokens.Add(" ");
                    else {
                        var prev = tokens.Last();
                        tokens.Add("$");
                        tokens.Add(prev);
                    }
                    continue;
                }
                if (tokenIdx != blankIdx)
                    tokens.Add(labels[tokenIdx]);
            }
            outputFeature.ReleaseFeature();
            // Return
            var result = string.Join(string.Empty, GroupBy(tokens)).Replace("$", string.Empty).Trim();
            return result;
        }
        #endregion


        #region --Operations--
        private readonly IMLModel model;
        private readonly string[] labels;
        private readonly int blankIdx, spaceIdx, twoIdx;

        void IDisposable.Dispose () { } // Not used

        private static IEnumerable<string> GroupBy (IEnumerable<string> tokens) {
            if (tokens.Count() == 0)
                yield break;
            var current = tokens.First();
            foreach (var item in tokens.Skip(1)) {
                if (item != current)
                    yield return current;
                current = item;
            }
            yield return current;
        }
        #endregion
    }
}