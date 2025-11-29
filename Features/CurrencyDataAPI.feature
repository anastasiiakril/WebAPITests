Feature:Currency Conversion



@tag1
Scenario: get convert usd to uah
	Given connected to currency data db
	And create convert request with amount "200" from "USD" to "UAH" for "2005-01-01"
	When send currency request
	Then conversion response is sucess
	And converted amount is returned
