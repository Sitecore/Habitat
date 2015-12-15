Feature: Contact Identification
	
As a sales person 
I want to show that Sitecore tracks the indentifiction of a visitor 
so that I can show the we know a person across visits

@NeedImplementation
Scenario: Accounts_Contact Identification_UC1_Identification is shown in the demo contact panel
	Given Habitat website is opened on Main Page
	When Actor clicks on <Info-sign> in the right down corner
	And Actor expands Identification section
	Then Contact ID term with it's ID presents
	And Identifier is empty
	And Identification status is <None>
	And System shows following info on Contact panel title
	| Number of visits | 
	| 1                | 


@NeedImplementation
Scenario: Accounts_Contact Identification_UC2_Identification is shown in the demo contact panel for just registered user
	Given Habitat website is opened on Register page
	And Actor entered following data in to the register fields
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Actor clicked Register button
	When User clicks on <Info-sign> in the right down corner
	And User expands Identification section
	Then Contact ID term with it's ID presents
	And Identifier is <extranet\kov@sitecore.net>
	And Identification status is <Known>
	And System shows following info on Contact panel title
	| Number of visits | 
	| 1                | 


@NeedImplementation
Scenario: Accounts_Contact Identification_UC3_Identification is shown in the demo contact panel for logged in user
	Given User is registered in Habitat and logged out 
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Habitat website is opened on Login page
	And System contained following info on Contact panel title
	| Number of visits | 
	| 1                | 
	When Actor enteres following data into Login page fields
	| Email              |Password |
	| kov@sitecore.net   |k        |
	And Actor clicks Login button on Login page
	And User clicks on <Info-sign> in the right down corner
	And User expands Identification section
	Then Contact ID term with it's ID presents
	And Identifier is <extranet\kov@sitecore.net>
	And Identification status is <Known>
	And System shows following info on Contact panel title
	| Number of visits | 
	| 2                | 


@NeedImplementation
Scenario: Accounts_Contact Identification_UC4_Start a new session
	Given User is registered in Habitat and logged in 
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Identification section in the demo contact panel is opened
	And Contact ID term with it's ID presented
	And Identifier was <extranet\kov@sitecore.net>
	And Identification status was <Known>
	And System shown following info on Contact panel title
	| Number of visits | 
	| 2                | 
	When User clicks END VISIT button
	And User clicks on <Info-sign> in the right down corner
	And User expands Identification section
	Then System shows the same Contact ID
	And System shows following info on Contact panel title
	| Number of visits | 
	| 3                | 


@NeedImplementation
Scenario: Accounts_Contact Identification_UC5_Clear browser cookies
Given User is registered in Habitat and logged in 
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Identification section in the demo contact panel is opened
	And Contact ID term with it's ID presented
	And Identifier was <extranet\kov@sitecore.net>
	And Identification status was <Known>
	And System shown following info on Contact panel title
	| Number of visits | 
	| 2                | 
	When clear browser cookies
	And login with following user
	| Email            | Password |
	| kov@sitecore.net | k        |
	And User clicks on <Info-sign> in the right down corner
	And User expands Identification section
	Then System shows the same Contact ID
	And Identifier is <extranet\kov@sitecore.net>
	And Identification status is <Known>
	And System shows following info on Contact panel title
	| Number of visits | 
	| 3                | 

	
