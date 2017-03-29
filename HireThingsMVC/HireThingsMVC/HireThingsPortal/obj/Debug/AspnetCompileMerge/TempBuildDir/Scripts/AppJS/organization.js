
function EditOrganization(id) {
    document.getElementById('HdOrgId').value = id;
    document.getElementById('OrganizationSearch').submit();
}



function OrganizationSearch() {
    var systemId = $("#ddlSystem").val();
    var organizationId = $("#ddlOrganization").val();
    var searchField = $("#ddlSearch").val();
    var searchText = $("#txtSearch").val();

    var container = $("#DivOrgGrid");
    $.ajax({
        type: "POST",
        url: "../OrganizationSearch/FilterOrganizationSearch",
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