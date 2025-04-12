package main

import (
	"fmt"
	"strconv"
)

func print(a ...any) (n int, err error)   { return fmt.Print(a...) }
func println(a ...any) (n int, err error) { return fmt.Println(a...) }
func debug(debug bool, a ...any) (n int, err error) {
	if debug {
		return fmt.Println(a...)
	}
	return 0, nil
}
func collate(c chan int) string {
	var res string = ""
	for x, ok := <-c; ok; x, ok = <-c {
		res += strconv.Itoa(x)
	}
	return res
}

func rabinowitzWagon(n int) chan int {
	c := make(chan int)
	go func() {
		var len = (10 * n) / 3;
		a := make([]int, len)
		for j := 0; j < len; j++ {
			a[j] = 2;
		}
		var nines = 0
		var predigit = 0
		var q = 0
		for j := 0; j < n; j++ {
			for i := len - 1; i > 0; i-- {
				var x = 10*a[i] + q*i
				a[i] = x % (2*i - 1)
				q = x / (2*i - 1)
			}
			a[1] = q % 10
			q = q / 10
			if q == 9 {
				nines++
			} else if q == 10 {
				c <- predigit + 1
				for k := 0; k < nines; k++ {
					c <- 0
				}
				predigit = 0
				nines = 0
			} else {
				c <- predigit
				predigit = q
				if nines != 0 {
					for k := 0; k < nines; k++ {
						c <- 9
					}
					nines = 0
				}
			}
		}
		c <- predigit
		close(c)
	}()
	<-c // skip leading zero of original algorithm
	return c
}

func main() {
	var N int = 45
	c := rabinowitzWagon(N)
	print(collate(c))
}
