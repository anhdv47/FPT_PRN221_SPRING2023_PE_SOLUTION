CommonFunction = {
  fetchData: function (url, callback) {
    fetch(url, {
      method: "GET",
    })
      .then(function (response) {
        return response.json();
      })
      .then(function (data) {
        callback(data);
      })
      .catch(function (error) {
        console.log(error);
      });
  },
};
