function TempDeviceSearch() {
    var systemId = $("#ddlSystem").val();
    var organizationId = $("#ddlOrganization").val();
    var searchField = $("#ddlSearch").val();
    var searchText = $("#txtSearch").val();

    var container = $("#DivTempGrid");
    $.ajax({
        type: "POST",
        url: "../TempDeviceSearch/FilterTempDeviceSearch",
        data: { SystemId: systemId, OrganizationId: organizationId, SearchField: searchField, SearchText: searchText }
    }).done(function (html) {
        container.html(html);
    })
    .fail(function (xhr, ajaxOptions, thrownError) {
        console.log(xhr);
        console.log(ajaxOptions);
        console.log(thrownError);
    });
}