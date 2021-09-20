
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
CORS(app)

# Pipeline-model:
# Extract sentence, languageCode, audioFileData and audioFile from incoming request.
# Check if a correct authorization key was given:
# Return mocked Response for Front-End-key, do the recognition for correct authorization-key.
# Build the audio file from the given data and persist it to Server.
# Do the recognition based on audiofile, reference sentence and languageCode.
# Calculate the levelOfDrunkenness based on the recognition result.
# Delete the temporarily persisted audiofile from the server.
# Return the calculated levelOfDrunkenness in the response.
@app.route("/recognition/audio", methods=["POST"])
def post_recognition():
	try:
		authorization = request.headers["authorization"]
		language_code = request.form["languageCode"]
		sentence = request.form["sentence"]
		audio_channel_count = int(request.form["audioChannelCount"])
		audio_bytes_per_sample = int(request.form["audioBytesPerSample"])
		audio_samplerate = int(request.form["audioSampleRate"])
		audio_file = request.files.get("audioFile")
	except (KeyError, ValueError):
		return "Invalid Request", status.HTTP_400_BAD_REQUEST

	# For Testing in Frontend-Development we do not want to use up our
	# 5 hours of free access to Azure Cognitive Services
	# so if we send the frontend-testing key we want to return
	# a mocked response
	if (authorization == keys.frontend_testing_key):
		return {"levelOfDrunkenness": 2}
	if(authorization != keys.authorization_key):
		return "Invalid Request", status.HTTP_400_BAD_REQUEST

	# Build and persist the audiofile.
	filename = build_audiofile(audio_file,
		audio_channel_count,
		audio_bytes_per_sample,
		audio_samplerate)

	# Do the recognition.
	recognition_result = recognizer.recognize_pronunciation(language_code,
		sentence,
		filename)

	# Calculate the levelOfDrunkenness, based on the recognition result.
	level_of_drunkenness = calculator.calculate_drunkenness(recognition_result)

	# Delete temporarily persisted audiofile from the server.
	os.remove(filename)

	# return the levelOfDrunkenness in the response.
	return {"levelOfDrunkenness" : level_of_drunkenness}

# Build the audiofile
# For two or more requests at the same time, we need to randomize the filename, so every file has a unique name.
# Otherwise a PermissionError would occur
# Returns the filename, by which the file can be accessed.
def build_audiofile(audio_file, audio_channel_count, audio_bytes_per_sample, audio_samplerate):
	filename_randomizer = random.randint(0, 9999999)
	filename = "pronunciation_file" + "_" + str(filename_randomizer) + ".wav"
	converter.convert_to_wave_and_save(filename, audio_file, audio_channel_count, audio_bytes_per_sample, audio_samplerate)
	return filename

# Runs the application server.
if __name__ == "__main__":
	app.run()