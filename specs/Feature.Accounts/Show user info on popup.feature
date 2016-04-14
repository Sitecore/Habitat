@UI
Feature: Show user info on popup
	
@Ready
Scenario: Account_Show user info on popup_UC1_Only email is shown 
	Given Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Actor clicks Register button
	And Actor selects User icon on Navigation bar
	Then User info is shown on User popup
	| Email            | Email lable |
	| kov@sitecore.net | E-mail      |


@Ready
Scenario: Account_Show user info on popup_UC2_Full user info is shown 
	Given User with following data is registered in Habitat
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |	
	And Edit profile page is opened
	When User inputs data in to the fields
	| LastName | FirstName  | PhoneNumber     |
	| Teltov   | Konstantin | +38(067)3333333 |
	And User clicks Update button on Edit User Profile page
	And Actor selects User icon on Navigation bar  
	Then User info is shown on User popup
	| Email            | Name              | Email lable | Name Lable |
	| kov@sitecore.net | Konstantin Teltov | E-mail      | Name       |


@Ready
Scenario: Account_Show user info on popup_UC3_User Name with special symbols 
	Given User with following data is registered in Habitat
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Edit profile page is opened	
	When User inputs data in to the fields
	| LastName            | FirstName               | PhoneNumber     |
	| Teltov!@#$%^&?()-+* | KONSTANTIN!@#$%^&?()-+* | +38(067)3333333 |
	And User clicks Update button on Edit User Profile page
	And Actor selects User icon on Navigation bar  
	Then User info is shown on User popup
	| Email            | Name                                        | Email lable | Name Lable |
	| kov@sitecore.net | Konstantin!@#$%^&?()-+* Teltov!@#$%^&?()-+* | E-mail      | Name       |


@Ready
Scenario: Account_Show user info on popup_Bug36103_Name label should dismiss if user has removed User Last Name and First Name
	Given User with following data is registered in Habitat
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Following fields were updated in User Profile
	| LastName | FirstName  |
	| Teltov   | Konstantin |
	And User clicks Update button on Edit User Profile page	
	When User updates following fields in User Profile 
	| LastName | FirstName |
	|          |           |
	And User clicks Update button on Edit User Profile page
	And Actor selects User icon on Navigation bar	
	Then User info is shown on User popup
	| Email            | Email lable | 
	| kov@sitecore.net | E-mail      | 
	And User info is not shown on User popup
	| Name Lable |Name              |
	| Name       |Konstantin Teltov |
	

 
