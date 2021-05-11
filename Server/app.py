from flask import Flask, request
from flask_restful import Api, Resource

app = Flask(__name__)

# extract sentence from incoming request
# extract the audiofile from incoming request
# return a mocked response according to defined API
@app.route("/recognition/audio", methods=["POST"])
def post_recognition():
	sentence = request.form.get("sentence")
	audio_file = request.files.get("audioFile")

	return {"levelOfDrunkenness" : 3}

# Debug = True only for testpurposes
if __name__ == "__main__":
	print("Server started")
	app.run(debug=True)