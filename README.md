# SeleniumAutoTestsProject
One test was realized in two different combinations (Nunit framework/ Nunit framework + SpecFlow):
1.Open Yandex Market
2. Go to Laptops page
3. Choose Lenovo from 25000 to 30000 price
4. Make sure that results are correct to the initial conditions.

To launch these tests you should install all nunit packages used in project (perhaps you should download chromedriver.exe and put it into bin\Debug\net5.0 folder of project).
Go to TestExplorer and launch any of existed tests ("SearchDefiniteCostAndManufacturerLaptopsShouldBeSuccess" or "Filter laptops on manufacturer and price").
