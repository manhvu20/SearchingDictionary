using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VuHoangManh_N20DCCN001_cuoiky_gtctdl
{
    class Meaning
    {
        public int id;
        public string mean;
        public Meaning next;

        public Meaning(int ID, string newMean)
        {
            id = ID;
            mean = newMean;
            next = null;
        }
    }
    class MeaningList
    {
        public int count;
        public Meaning head;
        public Meaning tail;
        public MeaningList()
        {
            count = 0;
            head = null;
            tail = null;
        }
        public int length()
        {
            return count;
        }
        public void addMeaning(string m)
        {
            Meaning newMean = new Meaning(count++, m);
            if (head == null)
            {
                head = newMean;
                tail = newMean;
            }
            else
            {
                tail.next = newMean;
                tail = newMean;
            }
        }
        public void deleteMeaning(string m)
        {
            Meaning p = head;
            while (p != null && p.mean != m)
            {
                p = p.next;
            }
            if (p.mean == m)
            {
                p.next = p.next.next;
                count -= 1;
                Console.WriteLine("Xóa nghĩa của từ thành công");
            }
            else
                Console.WriteLine("Không tìm thấy nghĩa của từ bạn muốn xóa");
        }
        public void printMeaningList()
        {
            Meaning current = head;
            while (current != null)
            {
                Console.WriteLine(current.mean);
                current = current.next;
            }
        }
        public void printMeaningListToFile(StreamWriter sw)
        {
            Meaning current = head;
            while (current != null)
            {
                sw.WriteLine("- " + current.mean);
                current = current.next;
            }
        }
        public string getMeaningListAsString()
        {
            StringBuilder sb = new StringBuilder();

            Meaning current = head;
            while (current != null)
            {
                sb.Append(current.mean);
                sb.Append(", ");
                current = current.next;
            }

            string result = sb.ToString();
            if (result.Length > 0)
            {
                result = result.Substring(0, result.Length - 2);
            }

            return result;
        }
    }
    class Example
    {
        public string example;
        public Example next;
        public Example(string e, Example n)
        {
            example = e;
            next = n;
        }
    }
    class ExamplesList
    {
        public Example head;
        public Example tail;
        private int size;

        public ExamplesList()
        {
            head = null;
            tail = null;
            size = 0;
        }
        public int length()
        {
            return size;
        }
        public bool isEmpty()
        {
            return size == 0;
        }
        public void addExample(string e)
        {
            Example newExample = new Example(e, null);
            if (isEmpty())
            {
                head = newExample;
                tail = newExample;
            }
            else
            {
                tail.next = newExample;
                tail = newExample;
            }
            size = size + 1;
        }
        public void deleteExample(string e)
        {
            Example p = head;
            while (p != null && p.example != e)
            {
                p = p.next;
            }
            if (p.example == e)
            {
                p.next = p.next.next;
                size -= 1;
                Console.WriteLine("Xóa ví dụ của từ thành công");
            }
            else
                Console.WriteLine("Không tìm thấy ví dụ của từ bạn muốn xóa");
        }
        public void printExamplesList()
        {
            Example current = head;
            while (current != null)
            {
                Console.WriteLine(current.example);
                current = current.next;
            }
        }
        public void printExamplesListToFile(StreamWriter sw)
        {
            Example current = head;
            while (current != null)
            {
                sw.WriteLine("- " + current.example);
                current = current.next;
            }
        }
        public string getExampleListAsString()
        {
            StringBuilder sb = new StringBuilder();

            Example current = head;
            while (current != null)
            {
                sb.Append(current.example);
                sb.Append(", ");
                current = current.next;
            }

            string result = sb.ToString();
            if (result.Length > 0)
            {
                result = result.Substring(0, result.Length - 2);
            }

            return result;
        }
    }
    class WordNode
    {
        public string word;
        public string type;
        public MeaningList means;
        public ExamplesList examples;
        public WordNode prev;
        public WordNode next;

        public WordNode(string word, string type)
        {
            this.word = word;
            this.type = type;
            means = new MeaningList();
            examples = new ExamplesList();
            prev = null;
            next = null;
        }

    }
    class Dictionary
    {
        private int size = 100;
        private WordNode[] hashTable;
        private WordNode first;
        private WordNode last;
        public int count;
        public Dictionary()
        {
            hashTable = new WordNode[size];
            first = null;
            last = null;
            count = 0;
        }
        private int HashFunction(string word) // Hàm băm để tính vị trí của từ trong mảng băm
        {
            int hash = 0;
            foreach (char c in word)
            {
                hash += (int)c;
            }
            return hash % size;
        }
        public bool isExisted(string word, WordNode[] nodes)
        {
            foreach (WordNode node in nodes)
            {
                if (node == null) continue;

                WordNode temp = node;
                while (temp != null)
                {
                    if (temp.word.Equals(word))
                    {
                        return true;
                    }
                    temp = temp.next;
                }
            }
            return false;
        }
        public int GetDictionaryCount()
        {
            int count = 0;
            for (int i = 0; i < size; i++)
            {
                WordNode curr = hashTable[i];
                while (curr != null)
                {
                    count++;
                    curr = curr.next;
                }
            }
            return count;
        }
        public void AddWord() // Hàm để thêm một từ mới vào từ điển
        {
            string word, type, meaning, example;
            WordNode node;

            // Đọc dữ liệu từ file
            WordNode[] nodes = ReadDataFromFile();

            // Tạo một node mới để chứa thông tin của từ
            Console.WriteLine("\n-------NHẬP TỪ MỚI-------");
            Console.Write("Nhập từ: ");
            word = Console.ReadLine();
            while (isExisted(word, nodes) == true)
            {
                Console.Write("Từ đã tồn tại! Vui lòng nhập từ khác: ");
                word = Console.ReadLine();
            }
            Console.Write("Nhập loại từ: ");
            type = Console.ReadLine();
            node = new WordNode(word, type);
            Console.WriteLine("Nhập các nghĩa của từ. Nhập EXIT nếu bạn muốn kết thúc nhập nghĩa!");
            while (true)
            {
                if (node.means.count > 4)
                {
                    Console.WriteLine("Quá giới hạn nhập nghĩa!!!");
                    break;
                }
                else
                {
                    Console.Write("Nhập nghĩa thứ " + (node.means.count + 1) + ": ");
                    meaning = Console.ReadLine();
                    if (meaning == "EXIT")
                    {
                        Console.WriteLine("Kết thúc nhập nghĩa!");
                        break;
                    }
                    else
                    {
                        node.means.addMeaning(meaning);
                    }
                }
            }
            Console.WriteLine("Nhập các ví dụ của từ. Nhập EXIT nếu bạn muốn kết thúc nhập ví dụ!");
            do
            {
                Console.Write("Nhập ví dụ: ");
                example = Console.ReadLine();
                if (example == "EXIT")
                {
                    Console.WriteLine("Kết thúc nhập ví dụ!");
                    break;
                }
                node.examples.addExample(example);
            } while (true);

            // Thêm node vào mảng băm
            if (hashTable[HashFunction(word)] == null)
            {
                hashTable[HashFunction(word)] = node;
            }
            else
            {
                WordNode curr = hashTable[HashFunction(word)];
                while (curr.next != null)
                {
                    curr = curr.next;
                }
                curr.next = node;
            }

            // Thêm node vào danh sách liên kết kép
            if (first == null)
            {
                first = node;
                last = node;
            }
            else
            {
                last.next = node;
                node.prev = last;
                last = node;
            }

            SaveDataToFile();
            Console.WriteLine("Thêm từ mới thành công!");
            TiepTucChuongTrinh();
        }
        public void DeleteWord()
        {
            string word;
            Console.WriteLine("\n-------XÓA TỪ-------");

            // Đọc dữ liệu từ file
            WordNode[] nodes = ReadDataFromFile();

            do
            {
                Console.Write("Nhập từ bạn muốn xóa: ");
                word = Console.ReadLine();

                if (isExisted(word, nodes) == false)
                {
                    Console.WriteLine("Từ không tồn tại trong từ điển! Vui lòng nhập lại: ");
                }
            } while (isExisted(word, nodes) == false);

            // Tìm vị trí băm của từ
            int index = HashFunction(word);

            WordNode curr = nodes[index];
            WordNode prev = null;
            while (curr != null && curr.word != word)
            {
                prev = curr;
                curr = curr.next;
            }

            // Xóa nút tại vị trí băm tương ứng với từ
            if (curr == null) // Trường hợp này không cần phải xóa
            {
                Console.WriteLine("Không tìm thấy từ cần xóa!");
                return;
            }
            else if (prev == null) // Nút cần xóa là nút đầu tiên trong danh sách liên kết
            {
                nodes[index] = curr.next;
            }
            else // Nút cần xóa nằm sau nút đầu tiên
            {
                prev.next = curr.next;
            }

            // Giảm giá trị của count
            count--;

            RemoveWordFromFile(word);
            Console.WriteLine("Đã xóa từ thành công!");

            // Cập nhật dữ liệu trên tệp văn bản
            SaveDataToFile();
            TiepTucChuongTrinh();
        }

        // Hàm để hiệu chỉnh một từ trong từ điển
        public void EditWord()
        {
            string input;
            int select;

            WordNode[] nodes = ReadDataFromFile();

            Console.WriteLine("\n-------HIỆU CHỈNH TỪ-------");
            do
            {
                Console.Write("Nhập từ bạn muốn hiệu chỉnh: ");
                input = Console.ReadLine();

                if (isExisted(input, nodes) == false)
                {
                    Console.WriteLine("Từ không tồn tại trong từ điển! Vui lòng nhập lại: ");
                }
            } while (isExisted(input, nodes) == false);

            WordNode node = hashTable[HashFunction(input)];

            int index = HashFunction(input);

            WordNode curr = nodes[index];
            WordNode prev = null;
            while (curr != null && curr.word != input)
            {
                prev = curr;
                curr = curr.next;
            }

            if (curr == null)
            {
                Console.WriteLine("Không tìm thấy từ cần xóa!");
                return;
            }
            else
            {
                Console.WriteLine("Từ bạn muốn hiệu chỉnh: " + curr.word + ". Nhập lựa chọn muốn hiệu chỉnh: ");
                Console.WriteLine("1. Sửa từ");
                Console.WriteLine("2. Sửa loại từ");
                Console.WriteLine("3. Sửa nghĩa của từ");
                Console.WriteLine("4. Sửa ví dụ");
                Console.WriteLine("Nhập lựa chọn của bạn: ");
                select = int.Parse(Console.ReadLine());
                try
                {
                    switch (select)
                    {
                        case 1:
                            Console.WriteLine("Nhập từ mới: ");
                            input = Console.ReadLine();
                            if (input == "")
                            {
                                Console.WriteLine("Từ mới không được để trống.");
                                return;
                            }
                            if (isExisted(input, nodes))
                            {
                                Console.WriteLine("Từ này đã tồn tại trong từ điển.");
                                return;
                            }
                            curr.word = input;

                            UpdateData(curr.word, nodes[index], index);
                            SaveDataToFile();
                            Console.WriteLine("Đã sửa từ thành công!");
                            break;

                        case 2:
                            Console.WriteLine("Nhập loại từ mới: ");
                            input = Console.ReadLine();
                            if (input != "")
                            {
                                node.type = input;
                            }
                            else
                            {
                                Console.WriteLine("Loại từ không được để trống.");
                            }
                            UpdateData(node.word, nodes[index], index);
                            SaveDataToFile();
                            Console.WriteLine("Đã sửa loại từ thành công!");
                            break;

                        case 3:
                            if (node.means.length() == 0)
                            {
                                Console.WriteLine("Không có nghĩa để sửa.");
                                return;
                            }

                            Console.WriteLine("Các nghĩa của từ:");
                            node.means.printMeaningList();
                            Console.WriteLine("1. Thêm nghĩa");
                            Console.WriteLine("2. Xóa nghĩa");
                            Console.WriteLine("Nhập lựa chọn của bạn: ");
                            select = int.Parse(Console.ReadLine());
                            switch (select)
                            {
                                case 1:
                                    Console.WriteLine("Nhập nghĩa bạn muốn thêm: ");
                                    input = Console.ReadLine();
                                    node.means.addMeaning(input);
                                    UpdateData(node.word, nodes[index], index);
                                    SaveDataToFile();
                                    Console.WriteLine("Thêm nghĩa mới thành công");
                                    break;
                                case 2:
                                    Console.WriteLine("Nhập nghĩa bạn muốn xóa: ");
                                    input = Console.ReadLine();
                                    node.means.deleteMeaning(input);
                                    UpdateData(node.word, nodes[index], index);
                                    SaveDataToFile();
                                    Console.WriteLine("Xóa nghĩa thành công");
                                    break;
                                default:
                                    Console.WriteLine("Lựa chọn không hợp lệ");
                                    break;
                            }
                            break;

                        case 4:
                            if (node.examples.length() == 0)
                            {
                                Console.WriteLine("Không có ví dụ để sửa.");
                                return;
                            }
                            Console.WriteLine("Các ví dụ của từ:");
                            node.examples.printExamplesList();
                            Console.WriteLine("1. Thêm ví dụ");
                            Console.WriteLine("2. Xóa ví dụ");
                            Console.WriteLine("Nhập lựa chọn của bạn: ");
                            select = int.Parse(Console.ReadLine());
                            switch (select)
                            {
                                case 1:
                                    Console.WriteLine("Nhập ví dụ bạn muốn thêm: ");
                                    input = Console.ReadLine();
                                    node.examples.addExample(input);
                                    UpdateData(node.word, nodes[index], index);
                                    SaveDataToFile();
                                    Console.WriteLine("Thêm ví dụ mới thành công");
                                    break;
                                case 2:
                                    Console.WriteLine("Nhập ví dụ bạn muốn xóa: ");
                                    input = Console.ReadLine();
                                    node.examples.deleteExample(input);
                                    UpdateData(node.word, nodes[index], index);
                                    SaveDataToFile();
                                    Console.WriteLine("Xóa ví dụ thành công");
                                    break;
                                default:
                                    Console.WriteLine("Lựa chọn không hợp lệ");
                                    break;
                            }
                            break;
                        default:
                            Console.WriteLine("Lựa chọn không hợp lệ.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                }
            }
        }
        public void UpdateData(string word, WordNode node, int index)
        {
            WordNode curr = hashTable[HashFunction(word)];
            WordNode prev = null;
            while (curr != null && curr.word != word)
            {
                prev = curr;
                curr = curr.next;
            }

            if (prev == null)
            {
                hashTable[HashFunction(word)] = node;
            }
            else
            {
                prev.next = node;
            }
        }
        public void LookUpWord()
        {
            string input;
            WordNode[] nodes = ReadDataFromFile();
            Console.WriteLine("\n-------TÌM KIẾM TỪ-------");
            do
            {
                Console.Write("Nhập từ bạn muốn tìm kiếm: ");
                input = Console.ReadLine();

                if (isExisted(input, nodes) == false)
                {
                    Console.WriteLine("Từ không tồn tại trong từ điển! Vui lòng nhập lại: ");
                }
            } while (isExisted(input, nodes) == false);

            // Mở file dữ liệu để đọc
            using (StreamReader reader = new StreamReader(@"C:\Users\Manh\Desktop\CTDLGT\dictionary_data.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] data = line.Split('|');

                    string word = data[0];
                    string type = data[1];
                    string[] meanings = data[2].Split(';');
                    string[] examples = data[3].Split(';');

                    if (word == input)
                    {
                        Console.WriteLine("Từ: " + word);
                        Console.WriteLine("Loại từ: " + type);
                        Console.WriteLine("Nghĩa:");
                        foreach (string meaning in meanings)
                        {
                            Console.WriteLine("- " + meaning.Trim());
                        }
                        Console.WriteLine("Ví dụ:");
                        foreach (string example in examples)
                        {
                            Console.WriteLine("- " + example.Trim());
                        }
                        break;
                    }
                }
            }
        }
        private void RemoveWordFromFile(string word)
        {
            string tempFile = @"C:\Users\Manh\Desktop\CTDLGT\temp.txt"; // Tạo ra 1 file temp.txt để ghi từ cần xóa

            using (var sr = new StreamReader(@"C:\Users\Manh\Desktop\CTDLGT\dictionary_data.txt"))
            using (var sw = new StreamWriter(tempFile))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    if (!line.StartsWith(word + "|"))
                    {
                        sw.WriteLine(line);
                    }
                }
            }

            File.Delete(@"C:\Users\Manh\Desktop\CTDLGT\dictionary_data.txt");
            File.Move(tempFile, @"C:\Users\Manh\Desktop\CTDLGT\dictionary_data.txt");
        }

        public WordNode[] ReadDataFromFile()
        {
            WordNode[] nodes = new WordNode[size];

            using (StreamReader reader = new StreamReader(@"C:\Users\Manh\Desktop\CTDLGT\dictionary_data.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] data = line.Split('|');

                    string word = data[0];
                    string type = data[1];
                    string[] meanings = data[2].Split(';');
                    string[] examples = data[3].Split(';');

                    WordNode node = new WordNode(word, type);
                    foreach (string meaning in meanings)
                    {
                        if (!string.IsNullOrEmpty(meaning.Trim()))
                        {
                            node.means.addMeaning(meaning.Trim());
                        }
                    }
                    foreach (string example in examples)
                    {
                        if (!string.IsNullOrEmpty(example.Trim()))
                        {
                            node.examples.addExample(example.Trim());
                        }
                    }

                    // Thêm nút vào bảng băm
                    int index = HashFunction(node.word);
                    if (nodes[index] == null)
                    {
                        nodes[index] = node;
                    }
                    else
                    {
                        WordNode curr = nodes[index];
                        while (curr.next != null)
                        {
                            curr = curr.next;
                        }
                        curr.next = node;
                    }

                    // Thêm nút vào danh sách liên kết kép
                    if (first == null)
                    {
                        first = node;
                        last = node;
                    }
                    else
                    {
                        last.next = node;
                        node.prev = last;
                        last = node;
                    }
                }
            }
            return nodes;
        }
        private void SaveDataToFile()
        {
            using (StreamWriter writer = new StreamWriter(@"C:\Users\Manh\Desktop\CTDLGT\dictionary_data.txt", true))
            {
                for (int i = 0; i < hashTable.Length; i++)
                {
                    WordNode node = hashTable[i];

                    while (node != null)
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.Append(node.word);
                        sb.Append("|");
                        sb.Append(node.type);
                        sb.Append("|");
                        sb.Append(node.means.getMeaningListAsString());
                        sb.Append("|");
                        sb.Append(node.examples.getExampleListAsString());
                        writer.WriteLine(sb.ToString());

                        // output each meaning associated with the word
                        Meaning currentMeaning = node.means.head;
                        while (currentMeaning != null)
                        {
                            currentMeaning = currentMeaning.next;
                        }

                        // output each example associated with the word
                        Example currentExample = node.examples.head;
                        while (currentExample != null)
                        {
                            currentExample = currentExample.next;
                        }

                        node = node.next;
                    }
                }
            }
        }
        public void TiepTucChuongTrinh()
        {
            int select;
            while (true)
            {
                Console.WriteLine("Bạn có muốn tiếp tục chương trình không?");
                Console.WriteLine("1. Tiếp tục");
                Console.WriteLine("0. Thoát");
                Console.Write("Nhập lựa chọn của bạn: ");
                select = int.Parse(Console.ReadLine());
                switch (select)
                {
                    case 1:
                        HienThiMenu();
                        break;
                    case 0:
                        Console.WriteLine("Thoát chương trình!");
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ! Vui lòng nhập lại: ");
                        break;
                }
                if (select == 0 || select == 1)
                    break;
            }
        }
        public void HienThiMenu()
        {
            int select;
            while (true)
            {
                Console.WriteLine("-------TRA CỨU TỪ ĐIỂN-------");
                Console.WriteLine("1. Thêm từ mới");
                Console.WriteLine("2. Xóa từ");
                Console.WriteLine("3. Hiệu chỉnh từ");
                Console.WriteLine("4. Tra từ");
                Console.WriteLine("0. Thoát");
                Console.Write("Nhập lựa chọn của bạn: ");
                select = int.Parse(Console.ReadLine());
                try
                {
                    switch (select)
                    {
                        case 1:
                            AddWord();
                            break;
                        case 2:
                            DeleteWord();
                            break;
                        case 3:
                            EditWord();
                            break;
                        case 4:
                            LookUpWord();
                            break;
                        case 0:
                            Console.WriteLine("Thoát chương trình!");
                            return;
                        default:
                            Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng nhập lại: ");
                            break;
                    }
                    if (select >= 0 && select <= 4)
                        break; // nếu đã nhập đúng lựa chọn thì thoát khỏi vòng lặp
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR: " + ex.Message);
                }
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.InputEncoding = System.Text.Encoding.UTF8;
                Dictionary dict = new Dictionary();
                dict.HienThiMenu();
                Console.ReadKey();
            }
        }
    }
}
