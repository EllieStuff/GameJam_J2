# Silero Speech-to-Text v3
Silero's [speech-to-text](https://github.com/snakers4/silero-models) machine learning model. This package requires [NatML](https://github.com/natsuite/NatML).

## Performing Speech to Text
First, create the predictor:
```csharp
// Fetch the model data from NatML Hub
var accessKey = "<HUB ACCESS KEY>"; // Get your access key from https://hub.natsuite.io/profile
var modelData = await MLModelData.FromHub("@natsuite/silero-stt-v3", accessKey);
// Deserialize the model
var model = modelData.Deserialize();
// Create the speech-to-text predictor
var predictor = new SpeechToTextv3Predictor(model, modelData.labels);
```

Then provide the predictor with a question and context:
```csharp
// Get an audio clip
// NOTE: The audio format of the audio clip/data must match `modelData.audioFormat`
AudioClip clip = ...;
// Predict
string result = predictor.Predict(clip);
```

## Requirements
- Unity 2019.2+
- [NatML 1.0+](https://github.com/natsuite/NatML)

## Resources
- Explore the [Silero models](https://github.com/snakers4/silero-models).
- See the [NatML documentation](https://docs.natsuite.io/natml).
- Join the [NatSuite community on Discord](https://discord.gg/y5vwgXkz2f).
- Discuss [NatML on Unity Forums](https://forum.unity.com/threads/natml-machine-learning-runtime.1109339/).
- Contact us at [hi@natsuite.io](mailto:hi@natsuite.io).

Thank you very much!