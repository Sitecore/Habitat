Feature: Person_Employee List
	

@NeedImplementation
Scenario: Person_Employee List_UC1_Default search query	
	Given Habitat website is opened on Main Page
	When Actor goes to Employees-List page	
	Then Following persons are shown 
	| Person      |
	| JOHN HOWARD |
	| JOHN DOE    |


@NeedImplementation
Scenario: Person_Employee List_UC2_Custom search query_text type was defined		
	Given Control properties were defined for item
	| Item path                                                            | Control ID                             | Data Source |
	| /sitecore/content/Habitat/Home/Modules/Feature/Person/Employees List | {DC19892A-B345-4EFE-AAA2-6E27A801A733} | text:CEO    |
	And Habitat website is opened on Main Page
	When Actor goes to Employees-List page	
	Then Following persons are shown 
	| Person      |
	| JOHN DOE    |

@NeedImplementation
Scenario: Person_Employee List_UC3_Custom search query_field type was defined	
	Given Control properties were defined for item
	| Item path                                                            | Control ID                             | Data Source                                     |
	| /sitecore/content/Habitat/Home/Modules/Feature/Person/Employees List | {DC19892A-B345-4EFE-AAA2-6E27A801A733} | template:{467c2144-4454-4518-b1de-e31b4cbbff33} |
	And Habitat website is opened on Main Page
	Then Following persons are shown 
	| Person      |
	| JOHN HOWARD |
	| JOHN DOE    |
	| $NAME       |  


@NeedImplementation
Scenario: Person_Employee List_UC4_Custom search query_field and text types were defined
	Given Control properties were defined for item
	| Item path                                                            | Control ID                             | Data Source                                               |
	| /sitecore/content/Habitat/Home/Modules/Feature/Person/Employees List | {DC19892A-B345-4EFE-AAA2-6E27A801A733} | text:John;template:{467c2144-4454-4518-b1de-e31b4cbbff33} |
	And Habitat website is opened on Main Page
	Then Following persons are shown 
	| Person      |
	| JOHN HOWARD |
	| JOHN DOE    |
	| $NAME       |


@NeedImplementation
Scenario: Person_Employee List_UC5_Custom search query_field and text types were defined_Include criteria logic
	Given Control properties were defined for item
	| Item path                                                            | Control ID                             | Data Source                                                |
	| /sitecore/content/Habitat/Home/Modules/Feature/Person/Employees List | {DC19892A-B345-4EFE-AAA2-6E27A801A733} | text:John;+template:{467c2144-4454-4518-b1de-e31b4cbbff33} |
	And Habitat website is opened on Main Page
	Then Following persons are shown 
	| Person      |
	| JOHN HOWARD |
	| JOHN DOE    |

@NeedImplementation
Scenario: Person_Employee List_UC6_Custom search query_Exclude criteria logic
	Given Control properties were defined for item
	| Item path                                                            | Control ID                             | Data Source         |
	| /sitecore/content/Habitat/Home/Modules/Feature/Person/Employees List | {DC19892A-B345-4EFE-AAA2-6E27A801A733} | text:John;-text:CEO |
	And Habitat website is opened on Main Page
	Then Following persons are shown 
	| Person      |
	| JOHN HOWARD |


@InDesign
Scenario: Person_Employee List_UC7_Custom search query_New version of the item is defined
	Given Control properties were defined for item
	| Item path                                                            | Control ID                             | Data Source                                               |
	| /sitecore/content/Habitat/Home/Modules/Feature/Person/Employees List | {DC19892A-B345-4EFE-AAA2-6E27A801A733} | text:John;template:{467c2144-4454-4518-b1de-e31b4cbbff33} |
	And New item version was added
	| Item name   |
	| John Doe    |
	Then Following persons are shown 
	| Person      |
	| JOHN HOWARD |
	| JOHN DOE    |


@InDesign
Scenario: Person_Employee List_UC8_Custom search query_New language of the item is defined
	Given Control properties were defined for item
	| Item path                                                            | Control ID                             | Data Source                                               |
	| /sitecore/content/Habitat/Home/Modules/Feature/Person/Employees List | {DC19892A-B345-4EFE-AAA2-6E27A801A733} | text:John;template:{467c2144-4454-4518-b1de-e31b4cbbff33} |
	And New item Language version was added
	| Item name   |
	| John Howard |
	Then Following persons are shown 
	| Person      |
	| JOHN HOWARD |
	| JOHN DOE    |


@NeedImplementation
Scenario: Person_Employee List_UC9_Default search query with custom Search Result Limit		
	Given Control properties were defined for item
	| Item path                                                            | Control ID                             | Data Source |
	| /sitecore/content/Habitat/Home/Modules/Feature/Person/Employees List | {DC19892A-B345-4EFE-AAA2-6E27A801A733} | text:CEO    |
	And Control properties were defined for item
	| Item path                                                            | Control ID                             | SearchResultLimit |
	| /sitecore/content/Habitat/Home/Modules/Feature/Person/Employees List | {DC19892A-B345-4EFE-AAA2-6E27A801A733} | 1                 |
	And Habitat website is opened on Main Page
	When Actor goes to Employees-List page	
	Then Following persons are shown
	| Person   |
	| JOHN DOE | 

