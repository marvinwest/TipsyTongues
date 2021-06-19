from flask import Flask, request
from flask_cors import CORS
from flask_api import status
import os

import wave_file_converter as converter
import pronunciation_recognizer as recognizer
import drunkenness_calculator as calculator
import server_keys as keys


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
	try:
		authentication = request.headers["authentication"]
		language_code = request.form["languageCode"]
		sentence = request.form["sentence"]
		audio_file = request.files.get("audioFile")
	except KeyError:
		return "Invalid Request", status.HTTP_400_BAD_REQUEST
	if(authentication != keys.authentication_key):
		return "Invalid Request", status.HTTP_400_BAD_REQUEST

	filename = "pronunciation_file.wav"
	converter.convert_to_wave_and_save(filename, audio_file)

	recognition_result = recognizer.recognize_pronunciation(language_code, sentence, filename)
	level_of_drunkenness = calculator.calculate_drunkenness(recognition_result)

	# delete temporary file after recognition
	os.remove(filename)

	print(__build_response(recognition_result))
	return {"levelOfDrunkenness" : level_of_drunkenness}

def __build_response(recognition_result):
	return {
		"accuracy": recognition_result.get_accuracy_score(),
		"completeness": recognition_result.get_completeness_score(),
		"fluency": recognition_result.get_fluency_score(),
		"aggregated": recognition_result.get_pronunciation_score()}

# app.run(<debug = True>) only for testpurposes
# Do not use it in deployment
if __name__ == "__main__":
	app.run(debug = True)