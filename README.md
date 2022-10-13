# “Headhunter’s candidates database” API

<br>

> API for tracking and organizing candidates for open positions.
> Using Sql server and .NET6
>
<br>

## API Reference

### Get collections

<br>

#### Get all candidates

```
  GET /api/candidates
```

<br>

#### Get all companies

```
  GET /api/companies
```

<br>

#### Get all positions

```
  GET /api/positions
```

<br>

#### Get companies where the candidate applied

```
  GET /api/candidate/{Id}/companies-applied-to
```

| Parameter | Type  | Description                   |
|:----------|:------|:------------------------------|
| `Id`      | `int` | **Required**. Id of candidate |

<br>

#### Get all open positions in the company

```
  GET /api/company/{id}/positions
```

| Parameter | Type  | Description                 |
|:----------|:------|:----------------------------|
| `Id`      | `int` | **Required**. Id of company |

<br>

### Get item

<br>

#### Get candidate by ID

```
  GET /api/candidate/{id}
```

| Parameter | Type  | Description                   |
|:----------|:------|:------------------------------|
| `id`      | `int` | **Required**. Id of candidate |

<br>

#### Get company by ID

```
  GET /api/company/{id}
```

| Parameter | Type  | Description                 |
|:----------|:------|:----------------------------|
| `id`      | `int` | **Required**. Id of company |

<br>

#### Get position by ID

```
  GET /api/position/{id}
```

| Parameter | Type  | Description                  |
|:----------|:------|:-----------------------------|
| `id`      | `int` | **Required**. Id of position |

<br>

## Delete item by id

<br>

#### Delete candidate by ID

```
  DELETE /api/candidate/{id}
```

| Parameter | Type  | Description                   |
|:----------|:------|:------------------------------|
| `id`      | `int` | **Required**. Id of candidate |

<br>

#### Delete company by ID

```
  DELETE /api/company/{id}
```

| Parameter | Type  | Description                 |
|:----------|:------|:----------------------------|
| `id`      | `int` | **Required**. Id of company |

<br>

#### Delete position by ID

```
  DELETE /api/position/{id}
```

| Parameter | Type  | Description                  |
|:----------|:------|:-----------------------------|
| `id`      | `int` | **Required**. Id of position |

<br>

### Add item

API consumes JSON raw request

<br>

#### Add candidate

```
  POST /api/candidate/
```

```json
{
  "fullName": "string",
  "email": "string",
  "about": "string",
  "skills": [
    "string"
  ]
}
```

| Parameter  | Type       | Description                          |
|:-----------|:-----------|:-------------------------------------|
| `fullName` | `string`   | **Required**. Full name of candidate |
| `email`    | `string`   | **Required**. Email of candidate     |
| `about`    | `string`   | Description of candidate             |
| `skills`   | `string[]` | Skill array of candidate             |

<br>

#### Add company

```
  POST /api/company/
```

```json
{
  "name": "string",
  "email": "string",
  "description": "string",
  "openPositions": [
    {
      "positionName": "string",
      "description": "string"
    }
  ]
}
```

| Parameter         | Type           | Description                         |
|:------------------|:---------------|:------------------------------------|
| `name`            | `string`       | **Required**. Full name of company  |
| `email`           | `string`       | **Required**. Email of company      |
| `description`     | `string`       | Description of company              |
| `openPositions`   | `Position[]`   | **Optional** Skill array of company |
| `positionName`    | `string`       | **Required**. Name of position      |
| `description`     | `string`       | Description of position             |

<br>

#### Add position

```
  POST /api/position/
```

```json
{
  "positionName": "string",
  "description": "string"
}
```

| Parameter      | Type     | Description                    |
|:---------------|:---------|:-------------------------------|
| `positionName` | `string` | **Required**. Name of position |
| `description`  | `string` | Description of position        |

<br>

#### Add position to company

```
  PUT /api/companyRequest/{id}/add-positionRequest
```

```json
{
  "positionName": "string",
  "description": "string"
}
```

| Parameter      | Type     | Description                    |
|:---------------|:---------|:-------------------------------|
| `id`           | `string` | **Required**. Company id       |
| `positionName` | `string` | **Required**. Name of position |
| `description`  | `string` | Description of position        |

<br>

### Add position by id

<br>

#### Add position to candidate

```
  PUT /api/candidate/{candidateId}/add-position-id/{positionId}
```

| Parameter     | Type  | Description                |
|:--------------|:------|:---------------------------|
| `candidateId` | `int` | **Required**. Candidate id |
| `positionId`  | `int` | **Required**. Position id  |

<br>

#### Add position to company

```
  PUT /api/company/{companyId}/add-position-id/{positionId}
```

| Parameter    | Type  | Description               |
|:-------------|:------|:--------------------------|
| `companyId`  | `int` | **Required**. Company id  |
| `positionId` | `int` | **Required**. Position id |

<br>

### Remove position by id

<br>

#### Remove position to candidate

```
  DELETE /api/candidate/{candidateId}/add-position-id/{positionId}
```

| Parameter     | Type  | Description                |
|:--------------|:------|:---------------------------|
| `candidateId` | `int` | **Required**. Candidate id |
| `positionId`  | `int` | **Required**. Position id  |

<br>

#### Remove position to company

```
  DELETE /api/company/{companyId}/add-position-id/{positionId}
```

| Parameter    | Type  | Description               |
|:-------------|:------|:--------------------------|
| `companyId`  | `int` | **Required**. Company id  |
| `positionId` | `int` | **Required**. Position id |

<br>

---







