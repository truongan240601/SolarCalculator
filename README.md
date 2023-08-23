# SolarCalculator
**By An**

# Mô tả 
Là ứng dụng unity 3d sử dụng thư viện Solar-Calculator của NOAA (https://gml.noaa.gov/) để tính toán các thông số vị trí của mặt trời thông qua các input mà người dùng nhập vào

# Cách triển khai

**Nghiên cứu thông tin**: sau khi nghiên cứu về việc tính toán vị trí của mặt trời thì có 2 hướng giải quyết 
  - Sử dụng các công thức thông qua các trang web uy tính như (https://gml.noaa.gov/ , https://midcdmz.nrel.gov/spa/).
  - Sử dụng thư viện giúp tính toán chuân sát và nhanh hơn (https://www.nuget.org/packages/SolarCalculator/3.3.0?_src=template).
=> sau khi nghiên cứu em thấy dùng thư viện sẽ chính sát hơn và việc code sẽ thuận tiện hơn.

**Phát triển sản phẩm**
  1. Tạo môi trường theo như yêu cầu của Level:
  2. Cài đặt các thư viện SolarCalculator thông qua package (https://github.com/GlitchEnzo/NuGetForUnity) vì nếu cài trực tiếp thư viện vào thì dẫn để máy tự động cài nhiều phiên bản của thư viện (hổ trợ nhiều loại .net khác nhau) -> unity sẽ không hiểu phải sử dụng phiên bản nào và sẽ không dùng được. 
  3. Test thư viện với debug.log
  4. Thiết kế UI.
  5. Hoàn thành sản phẩm.
