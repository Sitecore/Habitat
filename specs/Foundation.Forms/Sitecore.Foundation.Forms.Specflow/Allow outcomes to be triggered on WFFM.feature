@UI
Feature: Allow outcomes to be triggered on WFFM
	

@ManualOnly
Scenario: Allow outcomes to be triggered on WFFM_UC2_Assign Form Action
	Given Form Designer is opened for Web Form for Marketers
	And Form <Leave an Email> is selected
	And User clicks on Submit button of Form Designer
	And User selects Edit link on Submit Save Actions  
	When User selects <Register an Outcome> from Added Save Actions list
	And User clicks Edit button
	And User selects <Sales Lead> from list
	And User clicks OK button
	And User clicks Save and Close button on Form Designer	
	And User navigates to Forms page 
	And User enters <kovWFFM@sitecore.net> to the Email field
	And User clicks <Submit> button
	And Actor selects Open visit details panel slidebar
	And Actor expands Personal Information header on xDB panel
	Then <Outcomes> contains following values
	| field value |
	| Sales Lead  |
	


@ManualOnly
Scenario: Allow outcomes to be triggered on WFFM_UC2_Assign few Form Actions
	Given Form Designer is opened for Web Form for Marketers
	And Form <Leave an Email> is selected
	And User clicks on Submit button of Form Designer
	And User selects Edit link on Submit Save Actions  
	When User selects Save Actions combo-box 
	And User selects <Register an Outcome> action
	And User clicks <Add> button 
	And User selects <Register an Outcome> from Added Save Actions list
	And User clicks Edit button
	And User selects <Marketing Lead> from list
	And User clicks OK button
	And User clicks Save and Close button on Form Designer
	And User navigates to Forms page 
	And User enters <kovWFFM2@sitecore.net> to the Email field
	And User clicks <Submit> button
	And Actor selects Open visit details panel slidebar
	And Actor expands Personal Information header on xDB panel
	Then <Outcomes> contains following values
	| field value    |
	| Sales Lead     |
	| Marketing Lead |     



@ManualOnly
Scenario: Allow outcomes to be triggered on WFFM_UC3_Try to assign folder with outcomes 
	Given Form Designer is opened for Web Form for Marketers
	And Form <Leave an Email> is selected
	And User clicks on Submit button of Form Designer 
	And User selects <Register an Outcome> from Added Save Actions list
	And User clicks Edit button
	And User selects <Habitat> folder from list
	And User clicks OK button
	Then System shows warning message
	| Message                |
	| Please, select outcome | 



@ManualOnly 
Scenario: Allow outcomes to be triggered on WFFM_UC4_Assign outocome under some folder with outcomes
	Given Sitecore item should be created
	| Under item path                                           | Template ID                          | Item name | Group                                |
	| /sitecore/system/Marketing Control Panel/Outcomes/Habitat | EE43C2F0-6277-4144-B144-8CA2CEFCCF12 | QA Lead   | Outcome group/Lead management funnel |
	And Following outcome should be deployed
	| Under item path                                           |
	| /sitecore/system/Marketing Control Panel/Outcomes/Habitat |
	And Form Designer is opened for Web Form for Marketers
	And Form <Leave an Email> is selected
	And User clicks on Submit button of Form Designer
	And User selects Edit link on Submit Save Actions  
	When User selects <Register an Outcome> from Added Save Actions list
	And User clicks Edit button
	And User selects <QA Lead> from list
	And User clicks OK button
	And User clicks Save and Close button on Form Designer	
	And User navigates to Forms page 
	And User enters <kovWFFM3@sitecore.net> to the Email field
	And User clicks <Submit> button
	And Actor selects Open visit details panel slidebar
	And Actor expands Personal Information header on xDB panel
	Then Outcomes contains following values
	| field value |
	| QA Lead     |


@Ready 
Scenario: Allow outcomes to be triggered on WFFM_UC5_Triggger outcome on Demo form 
	Given Habitat website is opened on Forms page
	When Actor enteres following data into Leave an Email form fields
	| Email                   |
	| kovOutcome@sitecore.net |
	And Actor clicks Submit button on Leave an Email form
	And Actor selects Open visit details panel slidebar
	And Actor expands Onsite Behavior header on xDB panel
	Then Outcomes contains following values
	| field value            |
	| Now                    |
	| Lead management funnel |
	| Opportunity            |

	  