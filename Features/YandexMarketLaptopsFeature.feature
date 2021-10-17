Feature: YandexMarketLaptopsFeature
	Checks laptops filtered correctly according to conditionals.

@YandexMarket
Scenario: Filter laptops on manufacturer and price
	Given I have navigated to YandexMarket https://market.yandex.ru/ website
	And I transferred on laptops page
	When I choose manufacturer Lenovo
	And I choose lower price 25000
	And I choose upper price 30000
	Then I should get laptops according to manufacturer and entered prices