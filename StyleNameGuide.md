# Club Utils Style Guide
## Naming Conventions
### Class Names

UpperCamelCase: ExampleClassName

### Function Names

lowerCamelCase: exampleFunctionCall()

### Variable Names

lowerCamelCase with type: strExampleVariable, dblExampleVariable, intExampleVariable

### Constant Names

FULLCAPS: EXAMPLECONSTANT

### Dates

"YYYY-MM-DD"


## Database

### Databases

1. ClubUtilsDB

### Tables

1. Clubs:

| ID | ClubName | Balance |

2. Bulletins:

| ID | Heading | Timestamp | Text | ClubName |

3. Events:

| ID | EventName | EventTime | StopTime | ClubName | Recurring | (NOTE: if recurring, recurs weekly on original date)

4. Users:

| ID | FullName | Email | Password | ClubName | Rank | JoinDate |

5. Attendance:

| ID | ClubName | Date | FullName | Present |
