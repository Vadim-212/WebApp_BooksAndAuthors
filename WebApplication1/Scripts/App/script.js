
function GetTop5Users()
{
    $.ajax({
        type: `GET`,
        datatype: `json`,
        //url: `/Users/GetTop5Orders/`,
        url: `/User/GetTop5Orders/1`,
        //data: {
        //    userId: 1
        //},
        success: (data,textStatus,jqXHR) =>
        {
            console.log(data)
        },
        error: (result) => {
            app.alertDialog(`Error`,`Error while getting information`)
        }
    })
}