from flask import Flask, request
from flask_cors import CORS

app = Flask(__name__)
# CORS allows other programming languages to send a request
# that can be processed
CORS(app)

# extract sentence from incoming request
# extract the audiofile from incoming request
# return a mocked response according to defined API
@app.route("/recognition/audio", methods=["POST"])
def post_recognition():
	sentence = request.form.get("sentence")
	audio_file = request.files.get("audioFile")

	return {"levelOfDrunkenness" : 3}

# app.run(<Debug == True>) only for testpurposes
# Do not use it in deployment
if __name__ == "__main__":
	app.run()