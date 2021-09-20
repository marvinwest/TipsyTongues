# Static Map for threshold and levelOfDrunkenness.
drunkenness_thresholds = {95 : 0, 90 : 1, 85 : 2, 75 : 3, 0 : 4}

# Input: pronunciation_result with four attributes.
# Iterate over drunkenness_thresholds map.
# Returns LevelOfDrunkenness, if all attributes are above the according threshold.
def calculate_drunkenness(recognition_result):
	for key, value in drunkenness_thresholds.items():
		if (__is_threshold_met(recognition_result, key)):
			return value;

# Checks wether all attributes are above the threshold.
# Returns a boolean.
def __is_threshold_met(recognition_result, threshold):
	return (recognition_result.get_accuracy_score() >= threshold
		and recognition_result.get_completeness_score() >= threshold
		and recognition_result.get_fluency_score() >= threshold
		and recognition_result.get_pronunciation_score() >= threshold)