Feature: LCI_PostBinInspection

Scenario: inspection where bin exists in inventory
	Given URL for API is 'dlcis/api/v1/lcis/trans/bininspection'
	And Create POST Request
	And Payload is '..\..\DataSources\bininspection.json'
	When Execute the Request
	Then Status Code is 'Created'
	And Response Content

Scenario: inspection where exist in inventory
	Given a bin Id that already exists in inventory
	And Create POST Request
	And the Payload is set for PostBinInspection
	And URL for API is 'dlcis/api/v1/lcis/trans/bininspection'
	When the Request is executed
	Then Status Code is 'Created'
	And the response property 'Message' contains 'Request processed successfully'

Scenario: inspection where bin does not exist in inventory
	Given a bin Id that does not exist in inventory
	And Create POST Request
	And the Payload is set for PostBinInspection
	And URL for API is 'dlcis/api/v1/lcis/trans/bininspection'
	When the Request is executed
	Then Status Code is 'Created'
	And the response property 'Message' contains 'does not appear to be in inventory'