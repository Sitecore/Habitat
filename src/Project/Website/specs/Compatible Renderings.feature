Feature: Compatible Renderings
	As an editor 
	I want to be able to switch between layouts 
	So that I have flexibility in the layout of the pages


@NeedImplementation
Scenario: Compatible Renderings_UC1_Check compatible renderings in the Person module
	Given Expirience Editor application is launched
	And en/Modules/Feature/Person page is opened
	And EmployeeTeaser rendering is selected
	When User clicks <Replace with another component> button 
	And Selects EmployeeContactTeaser from <Select a replacement rendering> dialog
	Then rendering name changed to EmployeeContactTeaser
	And contact information is added in the bottom of the rendering


@NeedImplementation
Scenario: Compatible Renderings_UC2_Data source was not changed
	Given Expirience Editor application is launched
	And en/Modules/Feature/Accounts page is opened
	And PageSidebarLeft rendering is selected
	When User clicks <Replace with another component> button 
	And Selects PageSidebarRight from <Select a replacement rendering> dialog
	Then rendering name changed to PageSidebarRight
	And SecondaryMenu rendering is now on the right side of the page


@NeedImplementation
Scenario: Compatible Renderings_UC3_Check that where is no repeated renderings in the list
	Given Expirience Editor application is launched
	And Modules/Feature/Teasers page is opened
	When User selects <Inner 2 Column 6-6> rendering
	And User clicks <Replace with another component> button 
	Then following rendering available on the <Select a replacement rendering> dialog
	| Rendering Name     |
	| Inner 2 Column 4-8 |
	| Inner 2 Column 8-4 |
	| Inner 1 Column     |
