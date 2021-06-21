drunkenness_thresholds = {95 : 0, 90 : 1, 85 : 2, 75 : 3, 0 : 4}

def calculate_drunkenness(recognition_result):
	for key, value in drunkenness_thresholds.items():
		if (__is_threshold_met(recognition_result, key)):
			return value;

def __is_threshold_met(recognition_result, threshold):
	return (recognition_result.get_accuracy_score() >= threshold
		and recognition_result.get_completeness_score() >= threshold
		and recognition_result.get_fluency_score() >= threshold
		and recognition_result.get_pronunciation_score() >= threshold)