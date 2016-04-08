Feature: Contact Identification
	
As a sales person 
I want to show that Sitecore tracks the indentifiction of a visitor 
so that I can show the we know a person across visits

@Ready
Scenario: Accounts_Contact Identification_UC1_Anonymus user 
	Given Habitat website is opened on Main Page
	When Actor selects Open visit details panel slidebar
	And Actor expands Personal Information header on xDB panel
	Then User icon presents on Personal Information header section 
	And Personal Information header contains You are anonymous label 
	And Identification secret icon presents 

@Ready
Scenario: Accounts_Contact Identification_UC2_Identification is shown in the demo contact panel for just registered user
	Given User is registered in Habitat and logged out
	| Email                   | Password | ConfirmPassword |
	| kovContact@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email                   | Password |
	| kovContact@sitecore.net | k        |
	When Actor selects Open visit details panel slidebar
	And Actor expands Personal Information header on xDB panel
	Then User icon presents on Personal Information header section
	And Personal Information header contains You are known label
	And xDB Panel Body text contains
	| Text                             |
	| Email (Primary)                  |
	| kovContact@sitecore.net          |
	| Identification                   |
	| extranet\kovContact@sitecore.net |
	And Identification known icon presents



@Ready
Scenario: Accounts_Contact Identification_UC3_Start a new session
	Given User is registered in Habitat and logged out
	| Email                   | Password | ConfirmPassword |
	| kovContact@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email                   | Password |
	| kovContact@sitecore.net | k        |
	When Actor selects Open visit details panel slidebar
	And Actor clicks End Visit button on xDB panel
	And Actor selects Open visit details panel slidebar
	And Actor expands Personal Information header on xDB panel
	Then User icon presents on Personal Information header section
	And Personal Information header contains You are known label
	And xDB Panel Body text contains
	| Text                             |
	| Email (Primary)                  |
	| kovContact@sitecore.net          |
	| Identification                   |
	| extranet\kovContact@sitecore.net |
	And Identification known icon presents



@Ready
Scenario: Accounts_Contact Identification_UC4_Clear browser cookies
	Given User is registered in Habitat and logged out
	| Email                   | Password | ConfirmPassword |
	| kovContact@sitecore.net | k        | k               |
	And User was Login to Habitat
	| Email                   | Password |
	| kovContact@sitecore.net | k        |
	When Actor deletes all browser cookies 
	And Actor selects Open visit details panel slidebar
	And Actor expands Personal Information header on xDB panel
	Then User icon presents on Personal Information header section
	And Personal Information header contains You are known label
	And xDB Panel Body text contains
	| Text                             |
	| Email (Primary)                  |
	| kovContact@sitecore.net          |
	| Identification                   |
	| extranet\kovContact@sitecore.net |
	And Identification known icon presents



@Ready
Scenario: Accounts_Contact Identification_UC5_Inspect info for known user
	Given Habitat website is opened on Main Page
	And User was Login to Habitat
	| Email               | Password |
	| johnsmith@gmail.com | j        |
	When Actor selects Open visit details panel slidebar
	And Actor expands Personal Information header on xDB panel
	Then User icon presents on Personal Information header section
	And Personal Information header contains You are known label
	And xDB Panel Body text contains
	| Text                              |
	| Gender                            |
	| Male                              |
	| Job Title                         |
	| Intern                            |
	| Address (main)                    |
	| 153 SE 15th Rd Miami, , 33129 USA |
	| Email (Primary)                   |
	| JohnSmith@gmail.com               |
	| Phone (cell)                      |
	| +775 45454456                     |
	| Phone (work)                      |
	| +775 3434567653 ext 15            |
	| Preferred Language                |
	| en                                |
	| Identification                    |
	| extranet\JohnSmith@gmail.com      |
	And Identification known icon presents