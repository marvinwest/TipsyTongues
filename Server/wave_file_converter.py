import wave

def convert_to_wave_and_save(filename, byte_file):
	wave_file = wave.open(filename, "wb")
	wave_file.setnchannels(2)
	wave_file.setsampwidth(2)
	wave_file.setframerate(44100)
	wave_file.writeframesraw(byte_file.read())
	wave_file.close()