using System;
using System.IO;
using exercise1.Exceptions.StringCalculator;
using Xunit;

namespace exercise1.tests
{
    public class StringCalculatorUnitTests
    {
        [Fact]
        public void Add_returns_0_when_empty_string_provided()
        {
            int expected = 0;
            int result = StringCalculator.Add(String.Empty);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Add_returns_the_int_value_of_a_single_provided_term()
        {
            int expected = 2;
            string input = "2";

            int result = StringCalculator.Add(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Add_returns_the_summed_integer_value_of_two_provided_terms()
        {
            int expected = 3;
            string input = "1,2";

            int result = StringCalculator.Add(input);
            Assert.Equal(expected, result);
        }      


        [Fact]
        public void Add_returns_the_summed_integer_value_of_an_arbitrary_set_of_provided_terms()
        {
            int expected = 55;
            string input = "1,2,10,5,22,4,5,5,1";

            int result = StringCalculator.Add(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Add_returns_the_summed_integers_when_a_newline_is_present()
        {
            int expected = 6;
            string input = "1\n2,3";

            int result = StringCalculator.Add(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Add_returns_the_summed_integers_when_multiple_newlines_are_present()
        {
            int expected = 6;
            string input = "1\n\n2,3";

            int result = StringCalculator.Add(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Add_returns_the_summed_integers_when_single_delimiter_is_specified()
        {
            int expected = 15;
            string input = "//;\n2;3;10";

            int result = StringCalculator.Add(input);
            Assert.Equal(expected, result);
        }


        [Fact]
        public void Add_throws_error_when_a_negative_term_is_provided()
        {            
            string input = "//;\n-1;3;10";            
            var message = Assert.Throws<InvalidTermsException>(() => StringCalculator.Add(input));
            Assert.True(message.ToString().Equals("Negatives not allowed - -1"));
        }

        [Fact]
        public void Add_throws_error_when_multiple_negative_terms_are_provided_and_lists_each_invalid_term()
        {            
            string input = "//;\n-1;3;-10";            
            var message = Assert.Throws<InvalidTermsException>(() => StringCalculator.Add(input));
            Assert.True(message.ToString().Equals("Negatives not allowed - -1,-10"));
        }

        [Fact]
        public void Add_ignores_values_greater_than_1000()
        {     
            int expected = 4;       
            string input = "//;\n1;3;1001";            
            var termFilters = new Func<int,bool>[] { new Func<int,bool>(i => i <= 1000) };

            var result = StringCalculator.Add(input, termFilters);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Add_correctly_sums_data_from_a_sample_data_file_that_complies_to_the_ruleset ()
        {     
            int expected = 548;                    
            var termFilters = new Func<int,bool>[] { new Func<int,bool>(i => i <= 1000) };

            try
            {
                using (var sr = new StreamReader("data/test-data.txt"))
                {
                    var input = sr.ReadToEnd();
                    var result = StringCalculator.Add(input, termFilters);
                    Assert.Equal(expected, result);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }            
        }

        [Fact]
        public void Add_accepts_delimiters_with_length_greater_than_1_and_sums_correctly()
        {     
            int expected = 10;       
            string input = "//[;;;]\n1;;;3;;;6";   

            var result = StringCalculator.Add(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Add_accepts_multiple_delimiters_with_length_equal_to_1_and_sums_correctly()
        {     
            int expected = 15;       
            string input = "//[;][,][:]\n1;3,6:5";   

            var result = StringCalculator.Add(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Add_accepts_multiple_delimiters_with_length_greater_than_1_and_sums_correctly()
        {     
            int expected = 15;       
            string input = "//[;;][,,][::]\n1;;3,,6::5";   

            var result = StringCalculator.Add(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Add_accepts_multiple_delimiters_with_varying_lengths_and_sums_correctly()
        {     
            int expected = 15;       
            string input = "//[;;;][,,,,][::]\n1;;;3,,,,6::5";   

            var result = StringCalculator.Add(input);
            Assert.Equal(expected, result);
        }
    }
}
