1. 將下面的檔案放到對應的位置並加至專案中
kendo.common.min.css、kendo.default.min.css、Default 以及 font 資料夾放到專案中的 Content 底下
kendo.all.min.js、cultures 以及 messages 資料夾放到專案中的 Scripts 底下

2. 在 _layout.cshtml 中加入 kendo 的 js 和 css ( p.s. kendo相依於jq，請注意引入順序 )

3. 指定 kendo 的語系
https://docs.telerik.com/kendo-ui/framework/globalization/overview