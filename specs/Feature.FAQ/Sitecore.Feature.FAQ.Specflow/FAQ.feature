Feature: FAQ
	

@InDesign
Scenario: FAQ_UC1_Add new questions
	Given New FAQ questions were created and added
	| Under item path                      | Template ID                          | Item name | Question       | Answer                     |
	| /sitecore/content/Habitat/Global/FAQ | BFDC1F27-3D2D-495F-89A3-0951F882420B | FAQ3      | Who is Kostas? | Kostas is kov@sitecore.net |
	| /sitecore/content/Habitat/Global/FAQ | BFDC1F27-3D2D-495F-89A3-0951F882420B | FAQ4      | What is DST?   | DST is Demo Solution Team  |
	When User moves following items to selected in multilist field and saves changes
	| Item path                                                    | move item name |
	| /sitecore/content/Habitat/Home/Modules/Feature/FAQ/FAQ Group | FAQ3           |
	| /sitecore/content/Habitat/Home/Modules/Feature/FAQ/FAQ Group | FAQ4           |
	And User navigates to FAQ page
	Then Following questions with answers should be present on FAQ page 
	| Question       | Answer                     |
	| Who is Kostas? | Kostas is kov@sitecore.net |
	| What is DST?   | DST is Demo Solution Team  |


@InDesign
Scenario: FAQ_UC2_Add new group 
	Given New FAQ questions were created and added
	| Under item path                      | Template ID                          | Item name | Question            | Answer                |
	| /sitecore/content/Habitat/Global/FAQ | BFDC1F27-3D2D-495F-89A3-0951F882420B | FAQ5      | Who is MAN UTD fan? | Kostas is MAN UTD fan |
	When New FAQ group was created
	| Under item path                                    | Template ID                          | Item name   |
	| /sitecore/content/Habitat/Home/Modules/Feature/FAQ | BA1EB1BD-E705-4BE6-A432-06B7F1B2F2CC | FAQ Group 2 |
	And User moves following items to selected in multilist field and saves changes
	| Item path                                                      | move item name |
	| /sitecore/content/Habitat/Home/Modules/Feature/FAQ/FAQ Group 2 | FAQ5           |
	And User defines final layout on page
	| Item path                                           | Control       | Data Source                                                    |
	| /sitecore/content/Habitat/Home/Modules/Feature/FAQ/ | FAQ Accordion | /sitecore/content/Habitat/Home/Modules/Feature/FAQ/FAQ Group 2 |
	Then Following questions with answers should be present on FAQ page 
	| Question            | Answer                |
	| Who is MAN UTD fan? | Kostas is MAN UTD fan |


@InDesign
Scenario: FAQ_UC3_No questions available on page	 
	Given New FAQ group was created
	| Under item path                                    | Template ID                          | Item name   |
	| /sitecore/content/Habitat/Home/Modules/Feature/FAQ | BA1EB1BD-E705-4BE6-A432-06B7F1B2F2CC | FAQ Group 3 |
	When User defines final layout on page
	| Item path                                           | Control       | Data Source                                                    |
	| /sitecore/content/Habitat/Home/Modules/Feature/FAQ/ | FAQ Accordion | /sitecore/content/Habitat/Home/Modules/Feature/FAQ/FAQ Group 2 |
	Then Following questions with answers should not be present on FAQ page
	| Question            | Answer                     |
	| Who is MAN UTD fan? | Kostas is MAN UTD fan      |
	| Who is Kostas?      | Kostas is kov@sitecore.net |
	| What is DST?        | DST is Demo Solution Team  |










