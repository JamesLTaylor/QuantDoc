Writing docs
============

Maths
-----

Maths can be added to documents and labeled using native sphinx support like:

.. code-block:: RST

    .. math::
        α_t(i) = P(O_1, O_2, … O_t, q_t = S_i λ)
        :label: _math_example_formula

Which renders as 

.. math::
    α_t(i) = P(O_1, O_2, … O_t, q_t = S_i λ)
    :label: _math_example_formula

And can be referenced like this: 

.. code-block:: RST

    see equation :eq:`_math_example_formula`.

which yields: see equation :eq:`_math_example_formula`.

Sphinx should take care of the numbering through the document when we add a new equation like this one:

.. math:: e^{i\pi} + 1 = 0
   :label: euler