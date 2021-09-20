import azure.cognitiveservices.speech as speechsdk

from pronunciation_result import PronunciationResult
import server_keys as keys


# !!! DO NOT PUSH TO GITHUB WITH VALID speech_key !!!
# Speech-key: Your subscription key from Azure.
# Service_region: The region you declared in your Azure-profile.
speech_key, service_region = keys.azure_service_key, keys.azure_service_region

# Only functions for short audiofiles (max. 15 seconds).
# Uses Azure Congnitive Services to do a pronunciation recognition based on languageCode, reference sentence and audiofile.
# Returns PronunciationResult (Data transfer object, that holds the results of the recognition).
def recognize_pronunciation(language_code, sentence, filename):
	
	# Build necessary configs for pronunciation assesment
	speech_config = __build_speech_config()
	audio_config = __build_audio_config(filename)
	language_config = __build_language_config(language_code)
	speech_recognizer = __build_speech_recognizer(speech_config,
		language_config,
		audio_config)
	pronunciation_assessment_config = __build_pronunciation_assesment_config(sentence)
	pronunciation_assessment_config.apply_to(speech_recognizer)

	# Do the recognition.
	response = speech_recognizer.recognize_once()

	# Build PronunciationResult.
	try:
		result = __build_pronunciation_result(
			speechsdk.PronunciationAssessmentResult(response))
	except TypeError:
		# happens, when audiofile is empty
		result = PronunciationResult(0.0, 0.0, 0.0, 0.0)
	except AttributeError:
		# happens if nothing of the file could be recognized (gibberish speaking)
		result = PronunciationResult(0.0, 0.0, 0.0, 0.0)

	return result

# Build necessary SpeechConfig from subscription information.
def __build_speech_config():
	return speechsdk.SpeechConfig(subscription = speech_key, region = service_region)

# Build necessary AudioConfig from audiofilename.
def __build_audio_config(audio_file_name):
	return speechsdk.AudioConfig(filename = audio_file_name)

# Build neccessary SourceLanguageConfig from languageCode.
def __build_language_config(language_code):
	return speechsdk.languageconfig.SourceLanguageConfig(language_code)

# Builds the SpeechRecognizer from before built SpeechConfig, SourceLanguageConfig and AudioConfig.
def __build_speech_recognizer(speech_config, language_config, audio_config):
	return speechsdk.SpeechRecognizer(
		speech_config = speech_config,
		source_language_config = language_config,
		audio_config = audio_config)

# Build PronunciationAssessmentConfig from Sentence, that can then be applied to SpeechRecognizer.
# Grading System defines that the result values are between 0 - 100.
# Granularity Phoneme defines that the grading is based on fulltext, words and phonemes.
# Enable miscue activates failure calculation according to reference sentence and audiofile.
def __build_pronunciation_assesment_config(sentence):
	return speechsdk.PronunciationAssessmentConfig(
		reference_text = sentence,
		grading_system = speechsdk.PronunciationAssessmentGradingSystem.HundredMark,
		granularity = speechsdk.PronunciationAssessmentGranularity.Phoneme,
		enable_miscue = True)

# Build PronunciationResult from pronunciation assessment result
def __build_pronunciation_result(pronunciation_assessment_result):
	return PronunciationResult(
		pronunciation_assessment_result.accuracy_score,
		pronunciation_assessment_result.completeness_score,
		pronunciation_assessment_result.fluency_score,
		pronunciation_assessment_result.pronunciation_score)
