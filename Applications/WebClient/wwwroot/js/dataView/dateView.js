function InitializeDataView(filter, columnName, isDescending, pageNumber, password, uniqueMasterCitizenNumber, queryParameters) {
    // Fields
    const state = new State(filter, columnName, isDescending, pageNumber, password, uniqueMasterCitizenNumber, queryParameters);

    // Functions

    function initialize() {
        window.LoadResults = state.LoadResults.bind(state);

        // register event callbacks
        $(".column-header-arrow-container .glyphicon-triangle-top").click(onOrderArrowUpClick);
        $(".column-header-arrow-container .glyphicon-triangle-bottom").click(onOrderArrowDownClick);
        $(".sort-column-header").click(onColumnHeaderClick);

        $("#filter-button").click(onFilterButtonClick);
        $("#filter").keypress(onFilterInputKeypress);

        $("#unique-text").keypress(onUniqueKeypress);
        $("#pass-text").keypress(onPassKeypress);
        $("#pass-button").click(onPassButtonClick);

        $(".page-link").click(onPageLinkClick);

        if (state.ColumnName === null || state.ColumnName.length === 0) {
            return;
        }
        // set selected header and arrow class
        const $columnHeaderNames = $(`.sort-column-header .column-header-name`);
        const $selectedColumnHeaderName = $columnHeaderNames
            .filter((index, htmlElement) => $(htmlElement).text().trim() === state.ColumnName)

        const $selectedColumnHeader = $selectedColumnHeaderName.closest(".sort-column-header");
        $selectedColumnHeader.addClass("selected");

        const selectedArrowClassname = state.IsDescending
            ? ".glyphicon-triangle-bottom"
            : ".glyphicon-triangle-top";
        $selectedColumnHeader
            .find(selectedArrowClassname)
            .addClass("selected");
    }

    // Event callbacks

    function onOrderArrowUpClick($event) {
        const arrowElement = $event.currentTarget;
        const columnName = getColumnNameForArrowElement(arrowElement);
        state.ChangeColumnOrdering(columnName, false);
        $event.stopPropagation();
    }

    function onOrderArrowDownClick($event) {
        const arrowElement = $event.currentTarget;
        const columnName = getColumnNameForArrowElement(arrowElement);
        state.ChangeColumnOrdering(columnName, true);
        $event.stopPropagation();
    }

    function onColumnHeaderClick($event) {
        const columnHeader = $event.currentTarget;
        const columnName = getColumnNameForColumnHeader(columnHeader);
        state.ChangeColumnOrdering(columnName);
    }

    function onFilterButtonClick() {
        const filterInput = document.getElementById("filter");
        if (filterInput == null) {
            return;
        }
        state.ChangeFilter(filterInput.value);
    }
    function onPassButtonClick() {
        const pass = document.getElementById("pass-text").value;
        const unique = document.getElementById("unique-text").value;
        state.ChangeUniqueMasterCitizenNumber(unique);
        state.ChangePassword(pass);
    }

    function onFilterInputKeypress($event) {
        const keyCode = $event.which;
        if (keyCode == 13) { // enter keycode
            onFilterButtonClick();
        }
    }
    function onPassKeypress($event) {
        const keyCode = $event.which;
        if (keyCode == 13) { // enter keycode
            onPassButtonClick();
        }
    }
    function onUniqueKeypress($event) {
        const keyCode = $event.which;
        if (keyCode == 13) { // enter keycode
            onPassButtonClick();
        }
    }
    function onPageLinkClick($event) {
        const pageLinkElement = $event.currentTarget;
        const pageNumberString = $(pageLinkElement).text();
        const pageNumber = Number(pageNumberString);
        state.ChangePageNumber(pageNumber);
    }

    // Helper functions
    function getColumnNameForArrowElement(arrowElement) {
        const $columnHeader = $(arrowElement).closest(".sort-column-header");
        const columnName = getColumnNameForColumnHeader($columnHeader);
        return columnName;
    }

    function getColumnNameForColumnHeader(columnHeader) {
        const $columnHeaderName = $(columnHeader).children(".column-header-name").first();
        const columnHeaderText = $columnHeaderName.text();
        const columnName = columnHeaderText.trim();
        return columnName;
    }

    // Initialization
    $(document).ready(initialize);
}