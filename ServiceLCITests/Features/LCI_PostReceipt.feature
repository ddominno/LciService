Feature: LCI_PostReceipt

Scenario: Post Receipt where Bin Id does not already exist
	Given URL for API is 'dlcis/api/v1/lcis/trans/receipt'
	And Create POST Request
	And Payload is '..\..\DataSources\receipt.json'
	When Execute the Request
	Then Status Code is 'Created'
	And Response Content

Scenario: Post Receipt where Bin Id does not already exist 2
	Given URL for API is 'dlcis/api/v1/lcis/trans/receipt'
	And Create POST Request
	And the Payload is set for a new bin
	When the Request is executed
	Then Status Code is 'Created'
	And the response reads Processed Successfully
	And the bin appears in inventory

Scenario: Post Receipt where Bin Id already exist
	Given URL for API is 'dlcis/api/v1/lcis/trans/receipt'
	And the bin appears in inventory
	And the Payload is set for an existing bin
	And Create POST Request
	When the Request is executed
	Then Status Code is 'BadRequest'
	And the response property 'Message' contains 'already Exists'
	
