/* 
*   Speech to Text v3
*   Copyright (c) 2021 Yusuf Olokoba.
*/

namespace NatSuite.Examples {

    using UnityEngine;
    using UnityEngine.UI;
    using NatSuite.ML;
    using NatSuite.ML.Audio;

    public sealed class SpeechToTextv3Sample : MonoBehaviour {

        [Header(@"NatML Hub")]
        public string accessKey;

        [Header(@"Prediction")]
        public AudioClip clip;
        public Text text;

        async void Start () {
            // Playback the clip
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
            text.text = string.Empty;
            // Fetch the model data
            var modelData = await MLModelData.FromHub("@natsuite/silero-stt-v3", accessKey);
            // Deserialize the model
            var model = modelData.Deserialize();
            // Create speech-to-text predictor
            var predictor = new SpeechToTextv3Predictor(model, modelData.labels);
            // Predict
            var result = predictor.Predict(clip);
            // Visualize
            text.text = result;
            // Dispose the model
            model.Dispose();
        }
    }
}