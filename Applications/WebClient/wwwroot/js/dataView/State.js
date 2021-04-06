class State {
    constructor(filter, columnName, isDescending, pageNumber, password, uniqueMasterCitizenNumber, queryParameters) {
        this.Filter = filter;
        this.ColumnName = columnName;
        this.IsDescending = isDescending;
        this.Password = password;
        this.UniqueMasterCitizenNumber = uniqueMasterCitizenNumber;
        this.PageNumber = (pageNumber === undefined || pageNumber === null)
            ? 1
            : pageNumber;

        this.QueryParameters = (queryParameters === undefined || queryParameters === null)
            ? []
            : queryParameters;
    }

    // Methods
    ChangePassword(password) {
        if (this.Password === password) {
            return;
        }
        this.Password = password;
        this.PageNumber = 1;
        this.LoadResults();
    }
    ChangeUniqueMasterCitizenNumber(uniqueMasterCitizenNumber) {
        if (this.UniqueMasterCitizenNumber === uniqueMasterCitizenNumber) {
            return;
        }
        this.UniqueMasterCitizenNumber = uniqueMasterCitizenNumber;
    }
    ChangeFilter(filter) {
        if (this.Filter === filter) {
            return;
        }
        this.Filter = filter;
        this.PageNumber = 1;

        this.LoadResults();
    }

    ChangeColumnOrdering(columnName, isDescending) {
        if (isDescending === undefined) {
            isDescending = (this.ColumnName === columnName)
                ? !this.IsDescending
                : false;
        }

        this.ColumnName = columnName;
        this.IsDescending = isDescending;
        this.PageNumber = 1;

        this.LoadResults();
    }

    ChangePageNumber(pageNumber) {
        if (pageNumber < 1) {
            throw new Error("pageNumber must be a natural number.")
        }

        this.PageNumber = pageNumber;

        this.LoadResults();
    }

    LoadResults(additionalQueryParameters) {
        // additionalQueryParameters can be a string or an array
        let queryParameters = Array.from(this.QueryParameters);
        if (this.Filter !== null && this.Filter.length > 0) {
            const uriEncodedFilterString = encodeURIComponent(this.Filter);
            queryParameters.push(`filter=${uriEncodedFilterString}`);
        }
        if (this.Password != null && this.Password.length > 0) {
            queryParameters.push(`password=${this.Password}`);
        }
        if (this.UniqueMasterCitizenNumber != null && this.UniqueMasterCitizenNumber.length > 0) {
            queryParameters.push(`uniqueMasterCitizenNumber=${this.UniqueMasterCitizenNumber}`);
        }
        if (this.ColumnName !== null && this.ColumnName.length > 0) {
            const uriEncodedColumnNameString = encodeURIComponent(this.ColumnName);
            queryParameters.push(`columnName=${uriEncodedColumnNameString}`);
        }
        if (this.IsDescending) {
            queryParameters.push(`isDescending=${this.IsDescending}`);
        }
        if (this.PageNumber > 1) {
            queryParameters.push(`pageNumber=${this.PageNumber}`);
        }

        if (additionalQueryParameters !== undefined && additionalQueryParameters !== null) {
            queryParameters = queryParameters.concat(additionalQueryParameters);
        }

        let url = window.location.pathname;
        if (queryParameters.length > 0) {
            const queryParametersString = queryParameters.join("&");
            url += `?${queryParametersString}`;
        }
        window.location.href = url;
    }
}