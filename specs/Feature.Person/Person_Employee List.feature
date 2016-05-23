@UI
Feature: Person_Employee List
	

@Ready
Scenario: Person_Employee List_UC1_Default search query	
	Given Habitat website is opened on Main Page
	When Actor navigates to Employees-List page	
	Then Only following persons are shown 
	| Person      |
	| JOHN HOWARD |
	| JOHN DOE    |


@Ready
Scenario: Person_Employee List_UC2_Custom search query_text type was defined		
	Given Control properties were defined for item
	| ItemPath                                                             | ControlId                              | Type       | Value    |
	| /sitecore/content/Habitat/Home/Modules/Feature/Person/Employees List | {DC19892A-B345-4EFE-AAA2-6E27A801A733} | DataSource | text:CEO |
	And Habitat website is opened on Main Page
	When Actor navigates to Employees-List page	
	Then Only following persons are shown 
	| Person      |
	| JOHN DOE    |

@Ready
Scenario: Person_Employee List_UC3_Custom search query_field type was defined	
	Given Control properties were defined for item
	| Item path                                                            | Control ID                             | Type       | Value                                           |
	| /sitecore/content/Habitat/Home/Modules/Feature/Person/Employees List | {DC19892A-B345-4EFE-AAA2-6E27A801A733} | DataSource | template:{467c2144-4454-4518-b1de-e31b4cbbff33} |
	And Actor navigates to Employees-List page
	Then Only following persons are shown 
	| Person      |
	| JOHN HOWARD |
	| JOHN DOE    |


@Ready
Scenario: Person_Employee List_UC4_Custom search query_field and text types were defined
	Given Control properties were defined for item
	| Item path                                                            | Control ID                             | Type       | Value                                                     |
	| /sitecore/content/Habitat/Home/Modules/Feature/Person/Employees List | {DC19892A-B345-4EFE-AAA2-6E27A801A733} | DataSource | text:John;template:{467c2144-4454-4518-b1de-e31b4cbbff33} |
	And Actor navigates to Employees-List page
	Then Only following persons are shown 
	| Person      |
	| JOHN HOWARD |
	| JOHN DOE    |


@Ready
Scenario: Person_Employee List_UC5_Custom search query_field and text types were defined_Include criteria logic
	Given Control properties were defined for item
	| Item path                                                            | Control ID                             | Type       | Value                                                      |
	| /sitecore/content/Habitat/Home/Modules/Feature/Person/Employees List | {DC19892A-B345-4EFE-AAA2-6E27A801A733} | DataSource | text:John;+template:{467c2144-4454-4518-b1de-e31b4cbbff33} |
	And Actor navigates to Employees-List page
	Then Only following persons are shown 
	| Person      |
	| JOHN HOWARD |
	| JOHN DOE    |

@Ready
Scenario: Person_Employee List_UC6_Custom search query_Exclude criteria logic
	Given Control properties were defined for item
	| Item path                                                            | Control ID                             | Type       | Value               |
	| /sitecore/content/Habitat/Home/Modules/Feature/Person/Employees List | {DC19892A-B345-4EFE-AAA2-6E27A801A733} | DataSource | text:John;-text:CEO |
	And Actor navigates to Employees-List page
	Then Only following persons are shown 
	| Person      |
	| JOHN HOWARD |


@Ready
Scenario: Person_Employee List_UC7_Custom search query_New version of the item is defined
	Given Control properties were defined for item
	| Item path                                                            | Control ID                             | Type       | Value                                                     |
	| /sitecore/content/Habitat/Home/Modules/Feature/Person/Employees List | {DC19892A-B345-4EFE-AAA2-6E27A801A733} | DataSource | text:John;template:{467c2144-4454-4518-b1de-e31b4cbbff33} |	
	And New item version was added
	| Item path                                                                     | Language |
	| /sitecore/content/Habitat/Home/Modules/Feature/Person/Employees List/John Doe | de       |
	When Actor navigates to Employees-List page
	Then Only following persons are shown 
	| Person      |
	| JOHN HOWARD |
	| JOHN DOE    |


@Ready
Scenario: Person_Employee List_UC8_Custom search query_New language of the item is defined
	Given Control properties were defined for item
	| Item path                                                            | Control ID                             | Type       | Value                                                     |
	| /sitecore/content/Habitat/Home/Modules/Feature/Person/Employees List | {DC19892A-B345-4EFE-AAA2-6E27A801A733} | DataSource | text:John;template:{467c2144-4454-4518-b1de-e31b4cbbff33} |
	And New item version was added
	| Item path                                                            | Language |
	| /sitecore/content/Habitat/Home/Modules/Feature/Person/Employees List | en       |	
	And Value set to item field
	| ItemPath                                                                         | FieldName | FieldValue  |
	| /sitecore/content/Habitat/Home/Modules/Feature/Person/Employees List/John Howard | Name      | John Howard |
	When Actor navigates to Employees-List page
	Then Only following persons are shown 
	| Person      |
	| JOHN HOWARD |
	| JOHN DOE    |


@Ready
Scenario: Person_Employee List_UC9_Default search query with custom Search Result Limit		
	Given Control properties were defined for item
	| Item path                                                            | Control ID                             | Type       | Value    |
	| /sitecore/content/Habitat/Home/Modules/Feature/Person/Employees List | {DC19892A-B345-4EFE-AAA2-6E27A801A733} | DataSource | text:CEO |
	And Control properties were defined for item
	| Item path                                                            | Control ID                             | Type              | Value |
	| /sitecore/content/Habitat/Home/Modules/Feature/Person/Employees List | {DC19892A-B345-4EFE-AAA2-6E27A801A733} | SearchResultLimit | 1     |
	When Actor navigates to Employees-List page	
	Then Only following persons are shown
	| Person   |
	| JOHN DOE |
	

@Ready
Scenario: Person_Employee List_UC10_Default search query with null Search Result Limit		
	Given Control properties were defined for item
	| Item path                                                            | Control ID                             | Type       | Value                                           |
	| /sitecore/content/Habitat/Home/Modules/Feature/Person/Employees List | {DC19892A-B345-4EFE-AAA2-6E27A801A733} | DataSource | template:{467c2144-4454-4518-b1de-e31b4cbbff33} |
	And Control properties were defined for item
	| Item path                                                            | Control ID                             | Type              | Value |
	| /sitecore/content/Habitat/Home/Modules/Feature/Person/Employees List | {DC19892A-B345-4EFE-AAA2-6E27A801A733} | SearchResultLimit |       |
	When Actor navigates to Employees-List page	
	Then Only following persons are shown
	| Person      |
	| JOHN HOWARD |
	| JOHN DOE    |	 

