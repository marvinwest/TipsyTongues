from flask import Flask, request
from flask_cors import CORS

import pronunciation_recognizer as recognizer

app = Flask(__name__)
# CORS allows other programming languages to send a request
# that can be processed
CORS(app)

# extract sentence from incoming request
# extract the audiofile from incoming request
# return a mocked response according to defined API
@app.route("/recognition/audio", methods=["POST"])
def post_recognition():
	# language code to determine in which language the pronunciation regocnition should be
	# mocked for now
	# TODO: check: Results are quite high if german audio is used with english language_code
	# TODO: build full resultClass regarding response from Azure
	language_code = "en-US"
	sentence = request.form.get("sentence")
	audio_file = request.files.get("audioFile")

	recognition_result = recognizer.recognize_pronunciation(language_code, sentence, audio_file)

	print(__build_response(recognition_result))
	return {"levelOfDrunkenness" : 3}

def __build_response(recognition_result):
	return {
		"accuracy": recognition_result.get_accuracy_score(),
		"completeness": recognition_result.get_completeness_score(),
		"fluency": recognition_result.get_fluency_score(),
		"aggregated": recognition_result.get_pronunciation_score()}

# app.run(<Debug == True>) only for testpurposes
# Do not use it in deployment
if __name__ == "__main__":
	app.run()