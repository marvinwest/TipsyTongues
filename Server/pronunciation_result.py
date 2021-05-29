class PronunciationResult:
	def __init__(self, accuracy_score, completeness_score, fluency_score, pronunciation_score):
		self.__accuracy_score = accuracy_score
		self.__completeness_score = completeness_score
		self.__fluency_score = fluency_score
		self.__pronunciation_score = pronunciation_score

	
	def get_accuracy_score(self):
		return self.__accuracy_score

	def get_completeness_score(self):
		return self.__completeness_score

	def get_fluency_score(self):
		return self.__fluency_score

	def get_pronunciation_score(self):
		return self.__pronunciation_score
	
	