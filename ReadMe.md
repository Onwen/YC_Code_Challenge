# Yellow Canary Coding Challenge

## How to run

- simple .Net 8 app, to get the expected output run the console.exe.

## Assumptions

- Was not sure what format you wanted the data exported as, for simplicity i output as text to console. I imagine in a real life scenario it would be JSON document, CSV file or writing to a DB.
- Was not sure what kind of performance requirements the scenario had, so i opted for code readability. I could imagine a company like woolworths easily has large amounts of data (>10,000 employees *52 weekly payslips* 4+ paycodes per slip * 7 year historical data)
 	- In this case it may superior to read the file asynchronously and push out jobs that run on worker threads. (can we guarantee the data is ordered?)
- Was unsure of typical architecture internally to YC, opted for a library as this could be used in Rest API, Lambda, Worker Process etc.
 	- If Lambda was the path forward, i imagine we would pass the filepath in request and it would be reading xlsx from S3 bucket.

## Further Improvements

- Refactor code to pass in the filepath.
- need logger, i would probably opt for serilog.
- i would remove the adapter and work with the data in a similar format to DTO's.

## Conclusion

- i'm not particularly happy with this code, however i have to time-box to a few hrs on the weekend.
- wasted too much time testing out primary constructors.
- anything i missed would be appreciated as feedback.
