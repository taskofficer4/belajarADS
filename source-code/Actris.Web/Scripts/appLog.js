function appLog(code, user, screen, status, remarks, apikey, urlString) {
  var queryparams = {
    app_code: code, // diisi dengan application code masing-masing
    app_user: user,
    app_screen: screen, // set home
    app_status: status,
    remarks: remarks
  };

  // serialize the object into a query string
  var querystring = $.param(queryparams);

  console.log(querystring);

  // construct the URL with the query string
  var url = urlString + querystring;
  console.log(url);

  $.ajax({
    type: 'get',
    url: url,
    datatype: 'json',
    headers: {
      apikey: apikey
    },
    success: function(data) {
      // Handle success response
    },
    error: function(xhr, textstatus, errorthrown) {
      // Handle error response
    }
  }).done(function(data) {
    // Handle done response
  });
}