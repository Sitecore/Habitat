Feature: Show user info on popup
	
@NeedImplementation
Scenario: Account_Show user info on popup_UC1_Only email is shown 
	Given Habitat website is opened on Register page
	When Actor enters following data in to the register fields
	| Email            | Password | ConfirmPassword |
	| kov@sitecore.net | k        | k               |
	And Actor clicks Register button
	And Actor moves cursor over the User icon
	Then User info is shown on User popup
	| Email            | Email lable |
	| kov@sitecore.net | E-mail      |


@NeedImplementation
Scenario: Account_Show user info on popup_UC2_Full user info is shown 
	Given User with following data is registered in Habitat
	| Email              | Password | ConfirmPassword |
	| kov10@sitecore.net | k        | k               |	
	When Actor moves cursor over the User icon
	And User clicks Edit Profile from drop-down menu 
	And User inputs data in to the fields
	| Last Name | First Name | Phone number    |
	| Teltov    | Konstantin | +38(067)3333333 |
	And User selects <Swiming> from Interests drop-down list
	Then User info is shown on User popup
	| Email            | Name              | Email lable | Name Lable |
	| kov@sitecore.net | Konstantin Teltov | E-mail      | Name       |


@NeedImplementation
Scenario: Account_Show user info on popup_UC2_User Name with special symbols 
	Given User with following data is registered in Habitat
	| Email              | Password | ConfirmPassword |
	| kov10@sitecore.net | k        | k               |	
	When Actor moves cursor over the User icon
	And User clicks Edit Profile from drop-down menu 
	And User inputs data in to the fields
	| Last Name           | First Name              | Phone number    |
	| Teltov!@#$%^&?()-+* | KONSTANTIN!@#$%^&?()-+* | +38(067)3333333 |
	And User selects <Swiming> from Interests drop-down list
	Then User info is shown on User popup
	| Email            | Name                                        | Email lable | Name Lable |
	| kov@sitecore.net | Konstantin!@#$%^&?()-+* Teltov!@#$%^&?()-+* | E-mail      | Name       |


@NeedImplementation
Scenario: Account_Show user info on popup_Bug36103_Name label should dismiss if user has removed User Last Name and First Name
	Given User with following data is registered in Habitat and logged in 
	| Email            | Password | ConfirmPassword | First name | Last name |
	| kov@sitecore.net | k        | k               | Konstantin | Teltov    |
	When User moves cursor over the User icon
	And User clicks Edit Profile from drop-down menu 
	And User clears data in <First name> and <Last name> fields
	And User clicks Update button
	And When User moves cursor over the User icon
	Then User info is shown on User popup
	| Email            | 
	| kov@sitecore.net | 
	And Name label is no longer exists on the pop up
	

 
