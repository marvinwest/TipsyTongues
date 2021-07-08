import wave

def convert_to_wave_and_save(filename,
	byte_file,
	channel_count,
	bytes_per_sample,
	samplerate):
	wave_file = wave.open(filename, "wb")
	wave_file.setnchannels(channel_count)
	wave_file.setsampwidth(bytes_per_sample)
	wave_file.setframerate(samplerate)
	wave_file.writeframesraw(byte_file.read())
	wave_file.close()