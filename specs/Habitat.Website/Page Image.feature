Feature: Page Image
	 
@NeedImplementation
Scenario: Page Image_UC1_Make sure Page Image control available for PH col-narrow in Experience Editor
	Given Experience Editor application is launched
	And /Modules/Foundation/Sitecore-Extensions page is opened
	When User selects col-narrow-1 placeholder
	And User clicks <Add here> button
	And User selects <PageImage> rendering
	And user clicks Select button
	Then PageImage control inserted on the page

	@NeedImplementation
Scenario: Page Image_UC2_Make sure Page Image control available for PH col-wide in Experience Editor
	Given Experience Editor application is launched
	And /Modules/Feature/Person page is opened
	When User selects col-wide-1 placeholder
	And User clicks <Add here> button
	And User selects <PageImage> rendering
	And user clicks Select button
	Then PageImage control inserted on the page

@NeedImplementation
Scenario: Page Image_UC3_Verify PageImage control present in all Wide and Narrow columns
	Given User is logged to Sitecore as Admin
	When User selects Content Editor
	Then PageImage control presents in all Wide and Narrow columns
	| Column name  |
	| col-narrow-1 |
	| col-narrow-2 |
	| col-narrow-3 |
	| col-narrow-4 |
	| col-narrow-5 |
	| col-narrow-6 |
	| col-wide-1   |
	| col-wide-2   |
	| col-wide-3   |
	
@NeedImplementation
Scenario: Page Image_UC4_Make sure picture can be changed in control
	Given Experience Editor application is launched
	And /Modules/Feature/Person page is opened
	And PageImage rendering is inserted into col-wide-1 placeholder
	When User clicks <Edit related item> command
	And User sets </Default Website/cover> value into Image field
	And User saves changes
	Then changes applied on the /Modules/Feature/Person page

@NeedImplementation
Scenario: Page Image_UC5_Remove PageImage control
	Given Experience Editor application is launched
	And /Modules/Feature/Person page is opened
	And PageImage rendering is inserted into col-wide-1 placeholder
	When User selects <PageImage> rendering
	And User clicks <Remove> button
	And User saves changes on the page
	Then PageImage control is no longer present on the /Modules/Feature/Person page

	