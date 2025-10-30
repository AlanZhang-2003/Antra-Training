using System.Runtime.CompilerServices;

namespace ConsoleApp1;

public class oop
{
    public enum Grade
    {
        F = 0,
        D = 1,
        C = 2,
        B = 3,
        A = 4
    }
    public interface ICourseService
    {
        void Enroll(Student student);
        void AssignGrade(Student student, char grade);
    }

    public interface IPersonService<T> where T : Person
    {
        void AddPerson(Person person);
        void RemovePerson(int id);
        T GetPerson(int id);
        int GetAge(int id);
        decimal GetSalary(int id);

    }

    public interface IInstructorService : IPersonService<Instructor>
    {
        bool IsHead(int id);
        void AssignDepartment(int instructorId, string department);
        void ToggleHead(int id);
    }

    public interface IStudentService : IPersonService<Student>
    {
        void EnrollStudent(int studentId, int courseId);
        void AssignGrade(int studentId, int courseId, char grade);
        char GetGrade(int studentId, int courseId);
    }

    public interface IDepartmentService
    {
        void SetHead(Instructor instructor);
        Instructor GetHead();
        void SetBudgest(decimal budget, DateTime start, DateTime end);
        void AddCourse(Course course);
        List<Course> GetCourses();
    }
    
    
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public DateTime Birthday { get; set; }
        public List<string> Addresses  { get; set; }

        public Person(int id, string name, decimal salary, DateTime birthday, List<string> addresses = null)
        {
            if (salary < 0)
            {
                throw new ArgumentException("Salary cannot be negative");
            }
            
            Name = name;
            Salary = salary;
            Birthday = birthday;
            if (addresses != null)
                Addresses = addresses;
            else
                Addresses = new List<string>();
            
        }

        public int CalculateAge()
        {
            int age = DateTime.Now.Year - Birthday.Year;
            Console.WriteLine($"{age}");
            return age;
        }
        
        public virtual decimal CalculateSalary()
        {
            return Salary;
        }

        public string GetAddress()
        {
            Console.WriteLine($"Which address (total addresses: {Addresses.Count}) :");
            int index = int.Parse(Console.ReadLine());
            return Addresses[index];
        }
    }

    public class PersonService :IPersonService<Person>
    {
        private List<Person> _list = new List<Person>();

        public void AddPerson(Person person)
        {
            _list.Add(person);
        }

        public void RemovePerson(int id)
        {
            int index = _list.FindIndex(x => x.Id == id);
            _list.RemoveAt(index);
        }

        public Person GetPerson(int id)
        {
            return _list.Find(x => x.Id == id);
        }

        public int GetAge(int id)
        {
            Person person = GetPerson(id);
            return person.CalculateAge();
        }

        public decimal GetSalary(int id)
        {
            Person person = GetPerson(id);
            return person.CalculateSalary();
        }
    }

    public class Instructor : Person
    {
        public string Department;
        public bool Head;
        public DateTime JoinDate;

        public Instructor(int id, string name, decimal salary, DateTime birthday,
            string department,  bool head, DateTime joinDate, List<string> addresses = null) :
            base(id, name, salary, birthday, addresses)
        {
            Department = department;
            Head = head;
            JoinDate = joinDate;
        }

        public override decimal CalculateSalary()
        {
            int experience = DateTime.Now.Year - JoinDate.Year;
            decimal bonus = 5000 * experience;
            return Salary + bonus;
        }

    }

    public class InstructorService : IInstructorService
    {
        private List<Instructor> _list = new List<Instructor>();

        public void AddPerson(Person person)
        {
            if(person is Instructor instructor)
                _list.Add(instructor);
        }

        public void RemovePerson(int id)
        {
            int index = _list.FindIndex(x => x.Id == id);
            _list.RemoveAt(index);
        }

        public Instructor GetPerson(int id)
        {
            return _list.Find(x => x.Id == id);
        }

        public int GetAge(int id)
        {
            Instructor instructor = GetPerson(id);
            return instructor.CalculateAge();
        }

        public decimal GetSalary(int id)
        {
            Instructor instructor = GetPerson(id);
            return instructor.CalculateSalary();
        }

        public bool IsHead(int id)
        {
            Instructor instructor = GetPerson(id);
            return instructor.Head;
        }

        public void AssignDepartment(int instructorId, string department)
        {
            Instructor instructor = GetPerson(instructorId);
            if (instructor != null)
                instructor.Department = department;
        }

        public void ToggleHead(int id)
        {
            Instructor instructor = GetPerson(id);
            if (instructor != null)
                instructor.Head = !instructor.Head;
        }
    }

    public class Course : ICourseService
    {
        public int Id { get; set; }
        public string CourseName { get; set; }

        public List<Student> EnrolledStudents { get; set; } = new List<Student>();
        public Dictionary<int, char> Grades { get; set; } = new Dictionary<int, char>();

        public void Enroll(Student student)
        {
            if(!EnrolledStudents.Contains(student))
                EnrolledStudents.Add(student);
        }

        public void AssignGrade(Student student, char grade)
        {
            char[] validGrades = ['A', 'B', 'C', 'D', 'E', 'F'];
            
            char toUpperGrade = Char.ToUpper(grade);
            if (!validGrades.Contains(toUpperGrade))
            {
                Console.WriteLine("Grade is not valid");   
            }
            
            if (EnrolledStudents.Contains(student))
                Grades[student.Id] = toUpperGrade;
            else
                Console.WriteLine("Student does not exist");
        }
    }

    public class Student : Person
    {
        public int Id { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();
        
        public Student(int id, string name, decimal salary, DateTime birthday, List<string> addresses = null) :
            base(id, name, salary, birthday, addresses)
        {
        }

        public void CalculateGpa()
        {
            double total = 0;
            int count = 0;

            foreach (Course course in Courses)
            {
                if (course.Grades.TryGetValue(Id, out char grade))
                {
                    total += (int)grade;
                    count++;
                }
            }

            if (count == 0)
                Console.WriteLine($"GPA: 0");
            else
                Console.WriteLine($"GPA:{total / count}");
        }
    }


    public class StudentService : IStudentService
    {
        private List<Student> _students = new List<Student>();
        private List<Course> _courses = new List<Course>();
        
        public void AddPerson(Person person)
        {
            if(person is Student student)
                _students.Add(student);
        }

        public void RemovePerson(int id)
        {
            int index = _students.FindIndex(x => x.Id == id);
            _students.RemoveAt(index);
        }

        public Student GetPerson(int id)
        {
            return _students.Find(x => x.Id == id);
        }

        public int GetAge(int id)
        {
            Student student = GetPerson(id);
            return student.CalculateAge();
        }

        public decimal GetSalary(int id)
        {
            Student student = GetPerson(id);
            return student.CalculateSalary();
        }

        public void EnrollStudent(int studentId, int courseId)
        {
            Student student = GetPerson(studentId);
            Course course =  _courses.Find(c=>c.Id == courseId);

            if (student != null && course != null)
            {
                student.Courses.Add(course);
            }
            course.Enroll(student);
        }

        public void AssignGrade(int studentId, int courseId, Char grade)
        {
            Student student = GetPerson(studentId);
            Course course = _courses.Find(c => c.Id == courseId);

            if (student != null && course != null && course.EnrolledStudents.Contains(student))
                course.Grades[student.Id] = grade;
        }

        public char GetGrade(int studentId, int courseId)
        {
            Course course = _courses.Find(c => c.Id == courseId);

            if (course == null)
                return 'Z';

            if (course.Grades.TryGetValue(studentId, out char grade))
            {
                return grade;
            }

            return 'Z';

        }
    }

    public class Department : IDepartmentService
    {
        public Instructor Head { get; set; }
        public decimal Budget { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        private List<Course> _list = new List<Course>();
        
        public void SetHead(Instructor instructor)
        {
            Head = instructor;
        }

        public Instructor GetHead()
        {
            return Head;
        }

        public void SetBudgest(decimal budget, DateTime start, DateTime end)
        {
            Budget = budget;
            Start = start;
            End = end;
        }

        public void AddCourse(Course course)
        {
            if(!_list.Contains(course))
                _list.Add(course);
        }

        public List<Course> GetCourses()
        {
            return _list;
        }
    }
    

}