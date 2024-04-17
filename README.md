# BillPaymentSystem
![ERDiagram](https://github.com/RozerinSinem/BillPaymentSystemAPI/blob/master/ErDiagram.png)

Since the explanation in the paybill function is not descriptive enough, I did what made sense to me. If there is an unpaid bill, it converts it to paid in the database and returns the result "payment successful". However, if it has been paid, it returns the result "already paid". In my position, there is no such thing as paying a part of the fee so I did it this way.

Since there is no information about the totalAmount in the addbill section, I assumed that the phone bill is generally the same every month and assigned it 260.
