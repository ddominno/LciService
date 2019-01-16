Feature: LCI_GetBin

	Scenario: Test to get a bin Id that exists in inventory
	Given I have a Bin request 
	When I pass a Bin Id existing in Inventory
		And Execute the Request
	Then I should get status code 'OK'
		And a response with valid bin id

	Scenario: Test to get a bin Id that does not exist in inventory
	Given I have a Bin request 
	When I pass a Bin Id not existing in Inventory
		And Execute the Request
	Then I should get status code 'NotFound'
		And the response contains 'BinId not found'

	Scenario: Test to get a bin Id on inventory table but not in the DC
	Given I have a Bin request 
	When I pass a Bin Id existing in Inventory and Current Location Type is GrowerField
		And Execute the Request
	Then I should get status code 'NotFound'
		And the response contains 'BinId not found'