# H1
## H2
### H3

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

## Database

### Databases

1. ClubUtilsDB

### Tables

1. Clubs:

| ID | ClubName | Balance |

1. Bulletins:

| ID | Heading | Timestamp | Text | ClubName |

1. Events:

| ID | EventName | EventTime | ClubName |

1. Users:

| ID | FullName | Email | Password | Club | Rank |

1. Attendance:

| ID | Club | Date | FullName |
