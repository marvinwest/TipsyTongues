import azure.cognitiveservices.speech as speechsdk

from pronunciation_result import PronunciationResult


# !!! DO NOT PUSH TO GITHUB WITH VALID speech_key !!!

# Setup subscription info for the Speech Service
# speech-key: your subscription key from Azure
# service_region: The region you declared in your Azure-profile

speech_key, service_region = "add key here", "add service region here"

# only functions for short audiofiles.
# maybe use continuous recognition instead.
def recognize_pronunciation(language_code, sentence, filename):
	
	# build necessary configs for pronunciation assesment
	speech_config = __build_speech_config()
	audio_config = __build_audio_config(filename)
	language_config = __build_language_config(language_code)
	speech_recognizer = __build_speech_recognizer(speech_config, language_config, audio_config)
	pronunciation_assessment_config = __build_pronunciation_assesment_config(sentence)
	pronunciation_assessment_config.apply_to(speech_recognizer)

	response = speech_recognizer.recognize_once()

	try:
		result = __build_pronunciation_result(speechsdk.PronunciationAssessmentResult(response))
	except TypeError:
		# happens if nothing of the file could be recognized
		result = PronunciationResult(0.0, 0.0, 0.0, 0.0)

	return result

def __build_speech_config():
	return speechsdk.SpeechConfig(subscription = speech_key, region = service_region)

def __build_audio_config(audio_file_name):
	return speechsdk.AudioConfig(filename = audio_file_name)

def __build_language_config(language_code):
	return speechsdk.languageconfig.SourceLanguageConfig(language_code)

def __build_speech_recognizer(speech_config, language_config, audio_config):
	return speechsdk.SpeechRecognizer(
		speech_config = speech_config,
		source_language_config = language_config,
		audio_config = audio_config)

def __build_pronunciation_assesment_config(sentence):
	return speechsdk.PronunciationAssessmentConfig(
		reference_text = sentence,
		grading_system = speechsdk.PronunciationAssessmentGradingSystem.HundredMark,
		granularity = speechsdk.PronunciationAssessmentGranularity.Phoneme,
		enable_miscue = True)

def __build_pronunciation_result(pronunciation_assesment_result):
	return PronunciationResult(
		pronunciation_assesment_result.accuracy_score,
		pronunciation_assesment_result.completeness_score,
		pronunciation_assesment_result.fluency_score,
		pronunciation_assesment_result.pronunciation_score)