Feature: LCI_GetBinIncident
	
	Scenario: Test to Get incidents related to a bin in inventory
	Given I have a Bin request 
	When I pass a Bin Id existing in Inventory
		And Execute the Request
	Then I should get status code 'OK'
		And a response with valid bin id

	Scenario: Test to Get incidents where bin Id is not in inventory
	Given I have a Bin request 
	When I pass a Bin Id not existing in Inventory
		And Execute the Request
	Then I should get status code 'OK'
		And the response content is empty