Feature: Page Image
	 
@NeedImplementation
Scenario: Page Image_UC1_Make sure Page Image control available for PH col-narrow in Experience Editor
	Given Experience Editor application is opened
	And /Modules/Foundation/Sitecore-Extensions page is opened
	When User selects col-narrow-1 placeholder
	And User clicks <Add here> button
	And User selects <PageImage> rendering
	And User clicks Select button
	Then PageImage control presents on the page

	@NeedImplementation
Scenario: Page Image_UC2_Make sure Page Image control available for PH col-wide in Experience Editor
	Given Experience Editor application is opened
	And /Modules/Feature/Person page is opened
	When User selects col-wide-1 placeholder
	And User clicks <Add here> button
	And User selects <PageImage> rendering
	And User clicks Select button
	Then PageImage control presents on the page

@NeedImplementation
Scenario: Page Image_UC3_Verify PageImage control present in all Wide and Narrow columns
	Given User is logged to Sitecore as Admin
	When User selects Content Editor
	Then Items contain fields 
	| Item path                                                                 | Allowed Controls |
	| /sitecore/layout/Placeholder Settings/Project/Common/Columns/col-narrow-1 | PageImage        |
	| /sitecore/layout/Placeholder Settings/Project/Common/Columns/col-narrow-2 | PageImage        |
	| /sitecore/layout/Placeholder Settings/Project/Common/Columns/col-narrow-3 | PageImage        |
	| /sitecore/layout/Placeholder Settings/Project/Common/Columns/col-narrow-4 | PageImage        |
	| /sitecore/layout/Placeholder Settings/Project/Common/Columns/col-narrow-5 | PageImage        |
	| /sitecore/layout/Placeholder Settings/Project/Common/Columns/col-narrow-6 | PageImage        |
	| /sitecore/layout/Placeholder Settings/Project/Common/Columns/col-wide-1   | PageImage        |
	| /sitecore/layout/Placeholder Settings/Project/Common/Columns/col-wide-2   | PageImage        |
	| /sitecore/layout/Placeholder Settings/Project/Common/Columns/col-wide-3   | PageImage        |

	
@NeedImplementation
Scenario: Page Image_UC4_Make sure picture can be changed in control
	Given Experience Editor application is opened
	And /Modules/Features/Page Content page is opened
	And User selects col-narrow-1 placeholder
	And User clicks <Add here> button
	And User selects <PageImage> rendering
	And User clicks Select button
	When User selects PageImage rendering
	And User clicks <Edit related item> command
	And User selects new picture <Habitat-002-square>
	And User Save and Close button
	Then <Habitat-002-square> picture presents on page

@NeedImplementation
Scenario: Page Image_UC5_Remove PageImage control
	Given Experience Editor application is opened
	And /Modules/Features/Page Content page is opened
	And User selects col-narrow-1 placeholder
	And User clicks <Add here> button
	And User selects <PageImage> rendering
	And User clicks Select button
	When User selects <PageImage> rendering
	And User clicks <Remove> button
	And User saves changes on the page
	Then PageImage control is no longer present on the page

	