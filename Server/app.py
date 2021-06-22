
from flask import Flask, request
from flask_cors import CORS
from flask_api import status
import random
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
	try:
		authorization = request.headers["authorization"]
		language_code = request.form["languageCode"]
		sentence = request.form["sentence"]
		audio_file = request.files.get("audioFile")
	except KeyError:
		return "Invalid Request", status.HTTP_400_BAD_REQUEST

	# For Testing in Frontend-Development we do not want to use up our
	# 5 hours of free access to Azure Cognitive Services
	# so if we send the frontend-testing key we want to return
	# a mocked response
	if (authorization == keys.frontend_testing_key):
		return {"levelOfDrunkenness": 3}
	if(authorization != keys.authorization_key):
		return "Invalid Request", status.HTTP_400_BAD_REQUEST

	# For two or more requests at the same time, we need to randomize
	# the filename, so every file has a unique name
	# otherwise a PermissionError occurs
	filename_randomizer = random.randint(0, 9999999)
	filename = "pronunciation_file" + "_" + str(filename_randomizer) + ".wav"
	converter.convert_to_wave_and_save(filename, audio_file)

	recognition_result = recognizer.recognize_pronunciation(language_code, sentence, filename)
	level_of_drunkenness = calculator.calculate_drunkenness(recognition_result)

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