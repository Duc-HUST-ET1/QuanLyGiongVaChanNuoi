-- Thêm cột TrangThai (Kiểu Bit: True/False), mặc định là 1 (Hoạt động)
ALTER TABLE Nhom 
ADD TrangThai BIT DEFAULT 1 WITH VALUES;