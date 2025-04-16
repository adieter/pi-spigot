package main

// import (
// 	"fmt"
// 	"strconv"
// )

func rabinowitzWagon(n int) chan int {
	c := make(chan int)
	go func() {
		var len = (10 * n) / 3
		a := make([]int, len)
		for j := 0; j < len; j++ {
			a[j] = 2
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
