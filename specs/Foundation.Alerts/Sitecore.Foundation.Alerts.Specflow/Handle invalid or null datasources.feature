Feature: Handle invalid or null datasources
	

@NeedImplementation
Scenario: Handle invalid or null datasources_UC1_Experince editor view_Invalid datasource
	Given Datasource is defined for the Control 
	| ItemPath                       | ControlName | Data Source  |
	| /sitecore/content/Habitat/Home | Carousel    | Leeds United |
	When Habitat website is opened on Main Page in Edit mode
	Then Alert message is shown for item
	| Alert message                                                                                             |
	| Data source isn't set or have wrong template. Template {AE4635AF-CFBF-4BF6-9B50-00BE23A910C0} is required |



@NeedImplementation
Scenario: Handle invalid or null datasources_UC2_Normal mode_Invalid datasource
	Given Datasource is defined for the Control 
	| ItemPath                       | ControlName | Data Source  |
	| /sitecore/content/Habitat/Home | Carousel    | Leeds United |
	When Habitat website is opened on Main Page
	Then Item element absents on page


@NeedImplementation
Scenario: Handle invalid or null datasources_UC3_Experince editor view_null datasource
	Given Datasource is defined for the Control 
	| ItemPath                       | ControlName | Data Source |
	| /sitecore/content/Habitat/Home | Carousel    |             |
	When Habitat website is opened on Main Page in Edit mode
	Then Alert message is shown for item
	| Alert message                                                                                             |
	| Data source isn't set or have wrong template. Template {AE4635AF-CFBF-4BF6-9B50-00BE23A910C0} is required |



@NeedImplementation
Scenario: Handle invalid or null datasources_UC4_Normal mode_null datasource
	Given Datasource is defined for the Control 
	| ItemPath                       | ControlName | Data Source |
	| /sitecore/content/Habitat/Home | Carousel    |             |
	When Habitat website is opened on Main Page
	Then Item element absents on page
