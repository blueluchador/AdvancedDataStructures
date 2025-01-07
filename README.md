# Advanced Data Structures

Advanced data structures are essential for tackling complex computational challenges that go beyond the capabilities of basic structures like arrays or linked lists. They are designed to handle specific problems efficiently, such as fast prefix matching with Tries, spatial data organization with QuadTrees, or optimized priority queues with Fibonacci Heaps.  

In modern software development, these structures play a critical role in areas like big data processing, machine learning, and efficient database operations. By incorporating these advanced tools into development workflows, developers can achieve greater performance, solve specialized problems effectively, and build more scalable, responsive applications.

This repository is structured into categories based on the types of data structures, such as **Trees**, **Lookup Structures**, **Range Query Structures**, **Graphs**, and others. Some categories may contain subcategories to organize related structures further. Each project includes documentation detailing the purpose, use cases, and implementation of the data structures, offering a clear and organized way to explore and utilize them.

<!-- ![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

![Work in Progress](https://img.shields.io/badge/status-work%20in%20progress-yellow)

![Complete](https://img.shields.io/badge/status-complete-brightgreen) -->

## Trees
### `. AVL Tree
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Self-balancing tree for dynamic data.
- Ensures $O(\log n)$ operations for insertion, deletion, and search.

### 2. Red-Black Tree
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Used in associative containers like maps and sets.
- Balances dynamic data operations.

### 3. B-Tree
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Efficient for database indexing and retrieval.
- Supports range queries.

### 4. B+ Tree
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Optimized for range queries.
- Ideal for database indexing with linked leaf nodes.

### 5. Segment Tree
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Handles range queries for sums, minimums, or maximums in an efficient manner.

### 6. Fenwick Tree (Binary Indexed Tree)
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Optimized for prefix sums.
- Facilitates range updates.

### 7. QuadTree
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Efficient for partitioning spatial data in 2D.

### 8. Octree
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Efficient for partitioning spatial data in 3D.

### 9. KD-Tree
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Supports multidimensional range queries.
- Useful for nearest neighbor searches.

### 10. R-Tree
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Used for spatial indexing in geographic or multidimensional datasets.

### 11. Suffix Tree
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Optimized for text pattern matching.
- Facilitates substring queries.

### 12. Interval Tree
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Used for range queries.
- Detects interval overlaps.

### 13. Decision Tree
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Commonly used in machine learning.
- Facilitates classification and decision-making.

### 14. Cartesian Tree
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Designed for range minimum or maximum queries.

## Lookup Structures

The Lookup module is a core feature of **AdvancedDataStructures**, enabling fast and efficient data retrieval. 

For detailed documentation and examples, check out the [Lookups](https://github.com/blueluchador/AdvancedDataStructures/tree/main/src/AdvancedDataStructures.Lookups) module.

### 1. Trie (Prefix Tree)
![Complete](https://img.shields.io/badge/status-complete-brightgreen)

**Use Cases**
- Efficient for fast prefix matching, autocomplete, and dictionary lookups.

### 2. Radix Tree (Compressed Trie)
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Optimized for memory-efficient text and IP lookups.

### 3. Hash Table
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Supports fast key-based data retrieval.

### 4. Skip List
![Complete](https://img.shields.io/badge/status-complete-brightgreen)

**Use Cases**
- Probabilistically balances sorted data for fast search, insertion, and deletion.

### 5. Van Emde Boas Tree
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Supports successor/predecessor queries and min/max lookups in a fixed universe.

## Range Query Data Structures
### 1. Sparse Table
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Efficient for static range queries like minimum or maximum on immutable datasets.

### 2. Segment Tree
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Designed for range queries involving sums, minimums, or maximums.

### 3. Fenwick Tree
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Optimized for prefix sums and range updates.

## Graphs
### 1. Graph
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Used for network modeling, shortest path problems, and dependency management.

### 2. Dynamic Graph
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Handles dynamic connectivity and updates to graphs.

### 3. De Bruijn Graph
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Efficient for DNA sequencing and string matching problems.

### 4. Spanning Tree
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Optimized for finding minimum spanning trees in graphs.

## Heaps
### 1. Heap (Binary, Min-Heap, Max-Heap)
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Supports priority-based access and sorting algorithms like heapsort.

### 2. Fibonacci Heap
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Efficient for priority queues in graph algorithms such as Dijkstra and Prim.

## Probabilistic Structures
### 1. Bloom Filter
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Efficient for membership testing with low memory usage.

### 2. Count-Min Sketch
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Approximate counting for high-volume streaming data.

### 3. HyperLogLog
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Used for cardinality estimation, such as counting distinct elements in large datasets.

## Disjoint Sets
### 1. Disjoint Set (Union-Find)
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Efficient for dynamic connectivity problems and union-find operations.

## Hybrid Structures
### 1. Treap
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Combines binary search and heap balancing for efficient searching and probabilistic balancing.

### 2. Priority Deque
![Not Started](https://img.shields.io/badge/status-not%20started-lightgrey)

**Use Cases**
- Supports priority queue operations with efficient access to both minimum and maximum elements.
