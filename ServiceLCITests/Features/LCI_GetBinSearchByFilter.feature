Feature: LCI_GetBinSearchByFilter
	Scenario: Test to get bin list no filter
	Given I have a Bin request 
	When I pass no filter
		And Execute the Request
	Then I should get status code 'OK'
		And a response with valid bin id
	
	Scenario: Test to get a bin list filtered by GrowerNbr
	Given I have a Bin request 
	When I pass with GrowerNbr filter
		And Execute the Request
	Then I should get status code 'OK'
		And the response contains only records with specified grower number

			Scenario: Test to get a bin list filtered by RanchNbr
	Given I have a Bin request 
	When I pass with RanchNbr filter
		And Execute the Request
	Then I should get status code 'OK'
		And the response contains only records with specified ranch number

			Scenario: Test to get a bin list filtered by BerryType
	Given I have a Bin request 
	When I pass with BerryType filter
		And Execute the Request
	Then I should get status code 'OK'
		And the response contains only records with specified Berry Type

			Scenario: Test to get a bin list filtered by WarehouseNbr
	Given I have a Bin request 
	When I pass with WarehouseNbr filter
		And Execute the Request
	Then I should get status code 'OK'
		And the response contains only records with specified WarehouseNbr