Feature: Serialise users and roles


@NeedImplementation
Scenario: Serialise users and roles_UC1_Habitat modules roles available in Role Manager
	
	Then Following roles available
  | Role                                   |
	| habitat\Project Habitat Content Author |
	| habitat\Project Habitat Modules Admin  |
	| modules\Feature Accounts Admin         |
	| modules\Feature Demo Admin             |
	| modules\Feature FAQ Admin              |
	| modules\Feature Identity Admin         |
	| modules\Feature Language Admin         |
	| modules\Feature Maps Admin             |
	| modules\Feature Media Admin            |
	| modules\Feature Metadata Admin         |
	| modules\Feature Multisite Admin        |
	| modules\Feature Navigation Admin       |
	| modules\Feature News Admin             |
	| modules\Feature PageContent Admin      |
	| modules\Feature Person Admin           |
	| modules\Feature Search Admin           |
	| modules\Feature Social Admin           |
	| modules\Feature Teasers Admin          |
	| modules\Foundation Assets Admin        |
	| modules\Foundation Indexing Admin      |
	| modules\Foundation Multisite Admin     |


@NeedImplementation
Scenario: Serialise users and roles_UC2_Non-admin user without modules admin rights
	Given User habitat\UserRoles with u password and following roles created in Habitat
	| Role                                   |
	| habitat\Project Habitat Content Author |              

  Then habitat\UserRoles has Deny Write access to all available item fields
	#Then All available item fields are disabled


@NeedImplementation
Scenario: Serialise users and roles_UC3_Admin user with modules admin rights
	Given User habitat\UserRoles with u password and following roles created in Habitat
	| Role                                   |
	| habitat\Project Habitat Content Author |
	| habitat\Project Habitat Modules Admin  |             

#Then All available item fields are enabled
  Then habitat\UserRoles has  Write access to all available item fields


	@NeedImplementation
Scenario: Serialise users and roles_UC4_Admin user with multisite admin rights
	Given User habitat\UserRoles with u password and following roles created in Habitat
	| Role                                   |
	| habitat\Project Habitat Content Author |
	| modules\Feature Multisite Admin  |         

  Then habitat\UserRoles has  Write access to following item fields
  | Item name           |
	| DatasourceLocation  |
	| DatasourceTemplate  |




